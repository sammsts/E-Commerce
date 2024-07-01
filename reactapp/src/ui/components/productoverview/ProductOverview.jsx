import { useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom';
import api from '../../../api';
import { useNavigate } from 'react-router-dom';

export default function ProductOverview() {
    const location = useLocation();
    const params = new URLSearchParams(location.search);
    const serialId = params.get('serial');
    const [product, setProduct] = useState([]);
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [response, setResponse] = useState([]);
    const [user, setUser] = useState([]);
    const [address, setAddress] = useState([]);
    const [formData, setFormData] = useState({
        Car_Id: null,
        Ped_Id: null,
        Prd_Id: null,
        Usu_Id: null,
        End_Id: null,
        Car_Situacao: 0
    });

    const handleSubmit = (e) => {
        e.preventDefault();

        const fetchData = async () => {
            try {
                setLoading(true);

                const res = await api.post('/Carrinho/RegistrarCarrinho', formData);
                setResponse(res.data);
            } catch (error) {
                setError(error.response ? error.response.data.errors : error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    };

    useEffect(() => {
        if (!serialId) {
            navigate('/home');
        }

        const fetchProduct = async () => {
            try {
                const response = await api.get(`/Produto/SelecionarPorId/${serialId}`);
                const resUsuario = await api.get(`/Usuario/SelecionarPorEmail/${sessionStorage.getItem(`UserEmail`)}`)
                const resAddress = await api.get(`/Endereco/SelecionarPorId/${resUsuario.data.usu_id}`)
                setProduct(response.data);
                setUser(resUsuario.data);
                setAddress(resAddress.data);

                setFormData({
                    Car_Id: null,
                    Ped_Id: null,
                    Prd_Id: response.data.prd_id,
                    Usu_Id: resUsuario.data.usu_id,
                    End_Id: resAddress.data.end_id,
                    Car_Situacao: 0
                });
            } catch (error) {
                console.error('Erro ao buscar produtos:', error);
            }
        };

        fetchProduct();
    }, []);

    return (
        <div className="bg-white">
            <div className="pt-6">
                <nav aria-label="Breadcrumb">
                    <ol role="list" className="mx-auto flex max-w-2xl items-center space-x-2 px-4 sm:px-6 lg:max-w-7xl lg:px-8">
                        {product => (
                            <li key={product.prd_serialId}>
                                <div className="flex items-center">
                                    <a href={product.href} className="mr-2 text-sm font-medium text-gray-900">
                                        {product.prd_descricao}
                                    </a>
                                    <svg
                                        width={16}
                                        height={20}
                                        viewBox="0 0 16 20"
                                        fill="currentColor"
                                        aria-hidden="true"
                                        className="h-5 w-4 text-gray-300"
                                    >
                                        <path d="M5.697 4.34L8.98 16.532h1.327L7.025 4.341H5.697z" />
                                    </svg>
                                </div>
                            </li>
                        )}
                        <li className="text-sm">
                            <a href={product.href} aria-current="page" className="font-medium text-gray-500 hover:text-gray-600">
                                {product.prd_descricao}
                            </a>
                        </li>
                    </ol>
                </nav>

                {/* Image gallery */}
                <div className="mx-auto mt-6 max-w-2xl sm:px-6 lg:grid lg:max-w-7xl lg:grid-cols-3 lg:gap-x-8 lg:px-8">
                    <div className="aspect-h-4 aspect-w-3 hidden overflow-hidden rounded-lg lg:block">
                        <img
                            src={`data:image/jpeg;base64,${product.prd_imgProduto}`}
                            alt={product.prd_descricao}
                            className="h-full w-full object-cover object-center"
                        />
                    </div>
                    <div className="hidden lg:grid lg:grid-cols-1 lg:gap-y-8">
                        <div className="aspect-h-2 aspect-w-3 overflow-hidden rounded-lg">
                            <img
                                src={`data:image/jpeg;base64,${product.prd_imgProduto}`}
                                alt={product.prd_descricao}
                                className="h-full w-full object-cover object-center"
                            />
                        </div>
                        <div className="aspect-h-2 aspect-w-3 overflow-hidden rounded-lg">
                            <img
                                src={`data:image/jpeg;base64,${product.prd_imgProduto}`}
                                alt={product.prd_descricao}
                                className="h-full w-full object-cover object-center"
                            />
                        </div>
                    </div>
                    <div className="aspect-h-5 aspect-w-4 lg:aspect-h-4 lg:aspect-w-3 sm:overflow-hidden sm:rounded-lg">
                        <img
                            src={`data:image/jpeg;base64,${product.prd_imgProduto}`}
                            alt={product.prd_descricao}
                            className="h-full w-full object-cover object-center"
                        />
                    </div>
                </div>

                {/* Product info */}
                <div className="mx-auto max-w-2xl px-4 pb-16 pt-10 sm:px-6 lg:grid lg:max-w-7xl lg:grid-cols-3 lg:grid-rows-[auto,auto,1fr] lg:gap-x-8 lg:px-8 lg:pb-24 lg:pt-16">
                    <div className="lg:col-span-2 lg:border-r lg:border-gray-200 lg:pr-8">
                        <h1 className="text-2xl font-bold tracking-tight text-gray-900 sm:text-3xl">{product.prd_descricao}</h1>
                    </div>

                    {/* Options */}
                    <div className="mt-4 lg:row-span-3 lg:mt-0">
                        <h2 className="sr-only">Informação do produto</h2>
                        <p className="text-3xl tracking-tight text-gray-900">R${product.prd_valor}</p>

                        <form className="mt-10" onClick={ handleSubmit }>
                            <button
                                type="submit"
                                className="mt-10 flex w-full items-center justify-center rounded-md border border-transparent bg-indigo-600 px-8 py-3 text-base font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
                            >
                                {loading ? "Adicionando..." : "Adicionar ao carrinho"}
                            </button>
                        </form>
                        <div className="flex items-center justify-center sm:text-lg text-red-600">
                            <p>
                                {typeof error === 'string' && typeof error !== 'object' ? error : JSON.stringify(error)}
                            </p>
                        </div>
                    </div>

                    <div className="py-10 lg:col-span-2 lg:col-start-1 lg:border-r lg:border-gray-200 lg:pb-16 lg:pr-8 lg:pt-6">
                        {/* Description and details */}
                        <div>
                            <h3 className="sr-only">Descrição</h3>

                            <div className="space-y-6">
                                <p className="text-base text-gray-900">{product.prd_descricao}</p>
                            </div>
                        </div>

                        <div className="mt-10">
                            <h2 className="text-sm font-medium text-gray-900">Detalhes</h2>

                            <div className="mt-4 space-y-6">
                                <p className="text-sm text-gray-600">{product.prd_descricao}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
