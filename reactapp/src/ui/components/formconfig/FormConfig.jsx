import { UserCircleIcon } from '@heroicons/react/24/solid';
import { useNavigate } from 'react-router-dom';
import React, { useState, useEffect, useRef } from 'react';
import estadosJSON from '/src/helpers/Estados.json';
import cidadesJSON from '/src/helpers/Cidades.json';
import api from '/src/api.jsx';

export default function FormConfig() {
    const navigate = useNavigate();
    const fileInputRef = useRef(null);
    const [responseEnd, setResponseEnd] = useState([]);
    const [responseUsu, setResponseUsu] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [estados, setEstados] = useState([]);
    const [cidades, setCidades] = useState([]);
    const [indexIdEstado, setIndexIdEstado] = useState(null);
    const [formDataUsu, setFormDataUsu] = useState({
        Usu_id: null,
        Usu_nome: '',
        Usu_email: '',
        Usu_senha: '',
        Usu_idAdmin: false,
        Usu_ImgPerfil: null
    });
    const [formDataEnd, setFormDataEnd] = useState({
        primeiroNome: '',
        ultimoNome: '',
        Usu_email: '',
        End_pais: 'Brasil',
        End_estado: '',
        End_cep: '',
        End_bairro: '',
        End_rua: '',
        End_numero: '',
        End_complemento: '',
        End_cidade: '',
        foto: {
            nome: '',
            bytes: []
        }
    });

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (event) => {
                const base64String = event.target.result.replace("data:", "").replace(/^.+,/, "");
                setFormDataUsu((prevFormData) => ({
                    ...prevFormData,
                    Usu_ImgPerfil: base64String
                }));
            };
            reader.readAsDataURL(file);
        }
    };

    const handleClickFileInput = () => {
        fileInputRef.current.click();
    };

    const handleChange = (e) => {
        const { name, value, selectedIndex } = e.target;
        setFormDataEnd({ ...formDataEnd, [name]: value });
        setFormDataUsu({
            Usu_nome: formDataEnd.primeiroNome + ' ' + formDataEnd.ultimoNome,
            Usu_senha: '',
            Usu_idAdmin: false,
            Usu_email: formDataEnd.Usu_email,
            Usu_ImgPerfil: formDataEnd.foto.bytes
        });
        if (name === 'End_estado') {
            setIndexIdEstado(selectedIndex);
        }
    };

    const filterCities = () => {
        const selectedIndex = indexIdEstado;
        return cidades.filter(cidade =>
            cidade.Estado == selectedIndex
        );
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const fetchData = async () => {
            try {
                setLoading(true);
                const resEnd = await api.post('/Endereco/RegistrarEndereco', formDataEnd);
                const resUsu = await api.put('/Usuario/AtualizarUsuario', formDataUsu);
                setResponseEnd(resEnd.data);
                setResponseUsu(resUsu.data);

                setFormDataEnd({
                    primeiroNome: '',
                    ultimoNome: '',
                    Usu_email: '',
                    End_pais: 'Brasil',
                    End_estado: '',
                    End_cep: '',
                    End_bairro: '',
                    End_rua: '',
                    End_numero: '',
                    End_complemento: '',
                    End_cidade: '',
                    foto: {
                        nome: '',
                        bytes: null
                    }
                });
                setFormDataUsu({
                    Usu_nome: '',
                    Usu_email: '',
                    Usu_senha: '',
                    Usu_idAdmin: false,
                    Usu_ImgPerfil: null
                });
            } catch (error) {
                setError(error.response ? error.response.data.errors : error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    };

    const handleClickCancel = () => {
        navigate('/home');
    };

    useEffect(() => {
        setEstados(estadosJSON);
        setCidades(cidadesJSON);
    }, []);

    return (
        <form onSubmit={handleSubmit}>
            <div className="px-80 py-20 space-y-12">
                <h2 className="font-semibold leading-7 text-3xl font-bold hover:underline">Suas configura&ccedil;&otilde;es</h2>
                <div className="border-b border-gray-900/10">
                    <h2 className="text-base font-semibold leading-7">Perfil</h2>
                    <p className="mt-1 text-sm leading-6">
                        Essas informa&ccedil;&otilde;es ser&atilde;o exibidas publicamente, portanto, tenha cuidado com o que voc&ecirc; compartilha.
                    </p>

                    <div className="mt-10 grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-6">
                        <div className="col-span-full">
                            <label htmlFor="photo" className="block text-sm font-medium leading-6">
                                Foto
                            </label>
                            <div className="mt-2 flex items-center gap-x-3">
                                <UserCircleIcon className="h-12 w-12 text-gray-300" aria-hidden="true" />
                                <button
                                    type="button"
                                    className="rounded-md bg-white px-2.5 py-1.5 text-sm font-semibold text-black shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50"
                                    onClick={handleClickFileInput}
                                >
                                    Alterar
                                </button>
                                <input
                                    type="file"
                                    ref={fileInputRef}
                                    style={{ display: 'none' }}
                                    onChange={handleFileChange}
                                />
                            </div>
                            <label className="block italic text-sm">
                                { formDataEnd.foto.nome }
                            </label>
                        </div>
                    </div>
                </div>
                <div className="border-b border-gray-900/10 pb-12">
                    <h2 className="text-base font-semibold leading-7">Informa&ccedil;&otilde;es pessoais</h2>
                    <p className="mt-1 text-sm leading-6">Use um endere&ccedil;o permanente onde voc&ecirc; pode receber e-mails.</p>
                    <div className="mt-10 grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-6">
                        <div className="sm:col-span-3">
                            <label htmlFor="first-name" className="block text-sm font-medium leading-6">
                                Primeiro nome
                            </label>
                            <div className="mt-2">
                                <input
                                    type="text"
                                    name="primeiroNome"
                                    id="first-name"
                                    autoComplete="given-name"
                                    value={formDataEnd.primeiroNome}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                />
                            </div>
                        </div>

                        <div className="sm:col-span-3">
                            <label htmlFor="last-name" className="block text-sm font-medium leading-6">
                                &Uacute;ltimo nome
                            </label>
                            <div className="mt-2">
                                <input
                                    type="text"
                                    name="ultimoNome"
                                    id="last-name"
                                    autoComplete="family-name"
                                    value={formDataEnd.ultimoNome}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                />
                            </div>
                        </div>

                        <div className="sm:col-span-3">
                            <label htmlFor="email" className="block text-sm font-medium leading-6">
                                E-mail
                            </label>
                            <div className="mt-2">
                                <input
                                    id="email"
                                    name="Usu_email"
                                    type="email"
                                    autoComplete="email"
                                    value={formDataEnd.Usu_email}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                />
                            </div>
                        </div>

                        <div className="sm:col-span-3">
                            <label htmlFor="pais" className="block text-sm font-medium leading-6">
                                Pa&iacute;s
                            </label>
                            <div className="mt-2">
                                <select
                                    id="pais"
                                    name="End_pais"
                                    autoComplete="country-name"
                                    value={formDataEnd.End_pais}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                >
                                    <option>Brasil</option>
                                </select>
                            </div>
                        </div>

                        <div className=" sm:col-start-1 sm:col-span-1">
                            <label htmlFor="estado" className="block text-sm font-medium leading-6">
                                Estado
                            </label>
                            <div className="mt-2">
                                <select
                                    id="estado"
                                    name="End_estado"
                                    autoComplete="state-name"
                                    value={formDataEnd.End_estado}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:max-w-xs sm:text-sm sm:leading-6"
                                >
                                    <option value="">Selecione uma estado...</option>

                                    {estados.map((estado) => (
                                        <option key={estado.ID} value={estado.Sigla}>
                                            {estado.Nome}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="sm:col-span-1">
                            <label htmlFor="cidade" className="block text-sm font-medium leading-6">
                                Cidade
                            </label>
                            <div className="mt-2">
                                <select
                                    name="End_cidade"
                                    id="cidade"
                                    autoComplete="cidade-name"
                                    value={formDataEnd.End_cidade}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                >
                                    {filterCities().map(cidade => (
                                        <option key={cidade.ID} value={cidade.Nome}>
                                            {cidade.Nome}
                                        </option>
                                    ))}
                                </select>
                            </div>
                        </div>

                        <div className="sm:col-span-1">
                            <label htmlFor="postal-code" className="block text-sm font-medium leading-6">
                                CEP
                            </label>
                            <div className="mt-2">
                                <input
                                    type="text"
                                    name="End_cep"
                                    id="postal-code"
                                    autoComplete="postal-code"
                                    value={formDataEnd.End_cep}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                />
                            </div>
                        </div>

                        <div className="sm:col-span-1">
                            <label htmlFor="bairro" className="block text-sm font-medium leading-6">
                                Bairro
                            </label>
                            <div className="mt-2">
                                <input
                                    type="text"
                                    name="End_bairro"
                                    id="bairro"
                                    autoComplete="address-level2"
                                    value={formDataEnd.End_bairro}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                />
                            </div>
                        </div>

                        <div className="sm:col-span-1">
                            <label htmlFor="rua" className="block text-sm font-medium leading-6">
                                Rua
                            </label>
                            <div className="mt-2">
                                <input
                                    type="text"
                                    name="End_rua"
                                    id="rua"
                                    autoComplete="address-line1"
                                    value={formDataEnd.End_rua}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                />
                            </div>
                        </div>

                        <div className="sm:col-span-1">
                            <label htmlFor="numero" className="block text-sm font-medium leading-6">
                                N&uacute;mero
                            </label>
                            <div className="mt-2">
                                <input
                                    type="text"
                                    name="End_numero"
                                    id="numero"
                                    autoComplete="address-line2"
                                    value={formDataEnd.End_numero}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                />
                            </div>
                        </div>

                        <div className="col-span-full">
                            <label htmlFor="complemento" className="block text-sm font-medium leading-6">
                                Complemento
                            </label>
                            <div className="mt-2">
                                <input
                                    type="text"
                                    name="End_complemento"
                                    id="complemento"
                                    autoComplete="address-line3"
                                    value={formDataEnd.End_complemento}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 px-2 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                />
                            </div>
                        </div>
                    </div>
                </div>
                <div className="flex items-center justify-end gap-x-6">
                    <button type="button" className="text-sm font-semibold leading-6" onClick={handleClickCancel}>
                        Cancelar
                    </button>
                    <button
                        type="submit"
                        className="rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                    >
                        { loading ? "Salvando..." : "Salvar"}
                    </button>
                </div>
                <div className="flex items-center justify-center sm:text-lg text-red-600">
                    <p>
                        {typeof error === 'string' && typeof error !== 'object' ? error : JSON.stringify(error)}
                    </p>
                </div>
            </div>
        </form>
    );
}
