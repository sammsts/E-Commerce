import React, { useState, useRef } from 'react';
import api from '/src/api.jsx';

const Produtos = () => {
    const fileInputRef = useRef(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [response, setResponse] = useState([]);
    const [formData, setFormData] = useState({
        Prd_id: null,
        Prd_serialId: null,
        Prd_descricao: '',
        Prd_quantidadeEstoque: null,
        Prd_dataHoraCadastro: null,
        Prd_valor: null,
        Prd_imgProdutoBase64: '',
        Prd_imgProduto: null,
        Prd_foto: {
            nome: '',
            bytes: null
        }
    });

    const cleanBase64 = (base64String) => {
        if (base64String) {
            return base64String.replace(/"/g, '');
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleClickFileInput = () => {
        fileInputRef.current.click();
    };

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (event) => {
                const base64String = event.target.result.replace("data:", "").replace(/^.+,/, "");
                setFormData((prevFormData) => ({
                    ...prevFormData,
                    Prd_foto: {
                        nome: file.name,
                        bytes: base64String
                    },
                    Prd_imgProdutoBase64: base64String
                }));
            };

            reader.readAsDataURL(file);
        }
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const fetchData = async () => {
            try {
                setLoading(true);

                const res = await api.post('/Produto/RegistrarProduto', formData);
                setResponse(res.data);

                setFormData({
                    Prd_id: null,
                    Prd_serialId: '',
                    Prd_descricao: '',
                    Prd_quantidadeEstoque: '',
                    Prd_dataHoraCadastro: null,
                    Prd_valor: null,
                    Prd_imgProdutoBase64: '',
                    Prd_imgProduto: null,
                    Prd_foto: {
                        nome: '',
                        bytes: null
                    }
                });
            } catch (error) {
                setError(error.response ? error.response.data.errors : error.message);
            } finally {
                setLoading(false);
                setError(response);
            }
        };

        fetchData();
    };

    return (
        <div className="h-full grid">
            <div className="py-16 px-64">
                <h1 className="text-3xl font-bold hover:underline">Cadastrar Produtos</h1>
            </div>
            <form onSubmit={handleSubmit} className="px-7 grid justify-center items-center">
                <div className="grid gap-6" id="form">
                    <div className="w-full flex gap-3">
                        <input value={formData.Prd_serialId} onChange={handleChange} className="shadow-2xl p-3 ex w-full outline-none focus:border-solid focus:border-[1px] border-[#035ec5] placeholder:text-gray" type="number" placeholder="C&oacute;digo serial" id="Prd_serialId" name="Prd_serialId" required="" />
                    </div>
                    <div className="w-full flex gap-3">
                        <input value={formData.Prd_descricao} onChange={handleChange} className="shadow-2xl p-3 ex w-full outline-none focus:border-solid focus:border-[1px] border-[#035ec5] placeholder:text-gray" type="text" placeholder="Descri&ccedil;&atilde;o do produto" id="Prd_descricao" name="Prd_descricao" required="" />
                    </div>
                    <div className="grid gap-6 w-full">
                        <input value={formData.Prd_quantidadeEstoque} onChange={handleChange} className="p-3 shadow-2xl glass w-full outline-none focus:border-solid focus:border-[1px]border-[#035ec5] placeholder:text-gray" placeholder="Quantidade estoque" type="number" id="Prd_quantidadeEstoque" name="Prd_quantidadeEstoque" required="" />
                    </div>
                    <div className="grid gap-6 w-full">
                        <input value={formData.Prd_valor} onChange={handleChange} className="p-3 shadow-2xl glass w-full outline-none focus:border-solid focus:border-[1px]border-[#035ec5] placeholder:text-gray" placeholder="R$ Valor" type="number" id="Prd_valor" name="Prd_valor" required="" />
                    </div>
                    <div className="col-span-full">
                        <label htmlFor="photo" className="block text-sm font-medium leading-6">
                            Foto
                        </label>
                        <div className="mt-2 flex items-center gap-x-3">
                            <img src={`data:image/jpeg;base64,${cleanBase64(formData.Prd_foto.bytes)}`} className="h-12 w-12 text-gray-300" aria-hidden="true" />
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
                            {formData.Prd_foto.nome}
                        </label>
                    </div>
                    <button className="outline-none glass shadow-2xl  w-full p-3  bg-[#ffffff42] hover:border-[#035ec5] hover:border-solid hover:border-[1px]  hover:text-[#035ec5] font-bold" type="submit">{loading ? "Salvando..." : "Cadastrar"}</button>
                </div>
            </form>
            <div className="flex items-center justify-center sm:text-lg text-red-600">
                <p>
                    {typeof error === 'string' && typeof error !== 'object' ? error : JSON.stringify(error)}
                </p>
            </div>
        </div>
    );
};

export default Produtos;