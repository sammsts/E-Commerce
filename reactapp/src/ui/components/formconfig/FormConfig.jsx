import { UserCircleIcon } from '@heroicons/react/24/solid';
import { useNavigate } from 'react-router-dom';
import React, { useState, useEffect, useRef } from 'react';
import estadosJSON from '/src/helpers/Estados.json';
import cidadesJSON from '/src/helpers/Cidades.json';

export default function FormConfig() {
    const navigate = useNavigate();
    const fileInputRef = useRef(null);
    const [estados, setEstados] = useState([]);
    const [cidades, setCidades] = useState([]);
    const [indexIdEstado, setIndexIdEstado] = useState(null);
    const [formData, setFormData] = useState({
        primeiroNome: '',
        ultimoNome: '',
        email: '',
        pais: '',
        estado: '',
        cep: '',
        bairro: '',
        rua: '',
        numero: '',
        complemento: '',
        cidade: '',
        foto: {
            nome: '',
            bytes: null
        }
    });

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (event) => {
                const binaryString = event.target.result;
                const bytes = new Uint8Array(binaryString.length);
                for (let i = 0; i < binaryString.length; i++) {
                    bytes[i] = binaryString.charCodeAt(i);
                }
                setFormData({
                    ...formData,
                    foto: {
                        nome: file.name,
                        bytes: bytes
                    }
                });
            };
            reader.readAsBinaryString(file);
        }
    };

    const handleClickFileInput = () => {
        fileInputRef.current.click();
    };

    const handleChange = (e) => {
        const { name, value, selectedIndex } = e.target;
        setFormData({ ...formData, [name]: value });

        if (name === 'estado') {
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
        console.log('Dados do formulário:', formData);
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
                                { formData.foto.nome }
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
                                    value={formData.primeiroNome}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
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
                                    value={formData.ultimoNome}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
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
                                    name="email"
                                    type="email"
                                    autoComplete="email"
                                    value={formData.email}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
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
                                    name="pais"
                                    autoComplete="country-name"
                                    value={formData.pais}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
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
                                    name="estado"
                                    autoComplete="state-name"
                                    value={formData.estado}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:max-w-xs sm:text-sm sm:leading-6"
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
                                    name="cidade"
                                    id="cidade"
                                    autoComplete="cidade-name"
                                    value={formData.cidade}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                >
                                    {filterCities().map(cidade => (
                                        <option key={cidade.ID} value={cidade.Sigla}>
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
                                    name="cep"
                                    id="postal-code"
                                    autoComplete="postal-code"
                                    value={formData.cep}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
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
                                    name="bairro"
                                    id="bairro"
                                    autoComplete="address-level2"
                                    value={formData.bairro}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
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
                                    name="rua"
                                    id="rua"
                                    autoComplete="address-line1"
                                    value={formData.rua}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
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
                                    name="numero"
                                    id="numero"
                                    autoComplete="address-line2"
                                    value={formData.numero}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
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
                                    name="complemento"
                                    id="complemento"
                                    autoComplete="address-line3"
                                    value={formData.complemento}
                                    onChange={handleChange}
                                    className="block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
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
                        Salvar
                    </button>
                </div>
            </div>
        </form>
    );
}
