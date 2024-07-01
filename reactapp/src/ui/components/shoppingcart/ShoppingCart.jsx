import { useState, useEffect } from 'react'
import { Dialog, DialogPanel, DialogTitle, Transition, TransitionChild } from '@headlessui/react'
import { XMarkIcon } from '@heroicons/react/24/outline'
import api from '../../../api'

export default function ShoppingCart({ onClose }) {
    const [open, setOpen] = useState(true);
    const [products, setProducts] = useState([]);
    const [carAberto, setCarAberto] = useState([]);
    const [user, setUser] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [address, setAddress] = useState([]);
    const [response, setResponse] = useState([]);
    const [responseDelete, setResponseDelete] = useState([]);
    const [formData, setFormData] = useState({
        Ped_Id: null,
        Prd_Id: null,
        Usu_Id: null,
        End_Id: null,
        Ped_Quantidade: '',
        Ped_FormaPagamento: 0,
        Ped_Situacao: 0,
        Ped_DataPedido: null
    });

    const handleContinueShopping = () => {
        onClose();
    };

    const CalcSubTotal = () => {
        return products.reduce((total, product) => total + product.prd_valor, 0);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            setLoading(true);
            const res = await api.post('/Pedido/RegistrarPedido', formData);
            setResponse(res.data);

            const carrinhoAbertoDto = {
                Usu_id: formData.Usu_Id,
                End_id: formData.End_Id
            };

            await loadCar(carrinhoAbertoDto); // Espera até que o carrinho seja carregado após o pedido

            setFormData({
                Ped_Id: null,
                Prd_Id: null,
                Usu_Id: null,
                End_Id: null,
                Ped_Quantidade: null,
                Ped_FormaPagamento: 0,
                Ped_Situacao: 0,
                Ped_DataPedido: null
            });

            // Após o pedido ser registrado, atualiza os produtos (carrinho esvaziado)
            await fetchProduct();
        } catch (error) {
            setError(error.response ? error.response.data.errors : error.message);
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (prd_id) => {
        try {
            const responseDelete = await api.delete(`/Carrinho/DeletarCarrinho/${prd_id}`);
            setResponseDelete(responseDelete);

            const updatedProducts = products.filter(product => product.prd_id !== prd_id);
            setProducts(updatedProducts);
        } catch (error) {
            console.error('Erro ao excluir o carrinho:', error.response?.data || error.message);
        }
    };

    const handleButtonClick = (e) => {
        const prd_id = e.target.value;
        handleDelete(prd_id);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const loadCar = async (carrinhoAbertoDto) => {
        try {
            const obj = new URLSearchParams(carrinhoAbertoDto).toString();
            const resCarrinhoAberto = await api.get(`/Carrinho/SelecionarCarrinhoAberto?${obj}`);
            setCarAberto(resCarrinhoAberto.data);
        } catch (error) {
            console.error('Erro ao carregar carrinho:', error);
        }
    };

    const fetchProduct = async () => {
        try {
            const resUsuario = await api.get(`/Usuario/SelecionarPorEmail/${sessionStorage.getItem('UserEmail')}`);
            const resAddress = await api.get(`/Endereco/SelecionarPorId/${resUsuario.data.usu_id}`);

            const carrinhoAbertoDto = {
                Usu_id: resUsuario.data.usu_id,
                End_id: resAddress.data.end_id
            };

            const resCarrinhoAberto = await api.get(`/Carrinho/SelecionarCarrinhoAberto?${new URLSearchParams(carrinhoAbertoDto).toString()}`);
            setCarAberto(resCarrinhoAberto.data);

            const prdIds = resCarrinhoAberto.data.map(product => product.prd_Id);
            const params = prdIds.map(id => `id=${id}`).join('&');

            const resPrdsCarrinhoAberto = await api.get(`/Produto/SelecionarPorIds?${params}`);
            setProducts(resPrdsCarrinhoAberto.data);

            setFormData((prevFormData) => ({
                ...prevFormData,
                Prd_Id: prdIds,
                Usu_Id: resUsuario.data.usu_id,
                End_Id: resAddress.data.end_id,
                Ped_FormaPagamento: 0,
                Ped_Situacao: 1
            }));
        } catch (error) {
            console.error('Erro ao buscar produtos:', error);
        }
    };

    useEffect(() => {
        fetchProduct();
    }, [responseDelete]);

    return (
        <Transition show={open}>
            <Dialog className="relative z-10" onClose={setOpen}>
                <TransitionChild
                    enter="ease-in-out duration-500"
                    enterFrom="opacity-0"
                    enterTo="opacity-100"
                    leave="ease-in-out duration-500"
                    leaveFrom="opacity-100"
                    leaveTo="opacity-0"
                >
                    <div className="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" />
                </TransitionChild>

                <div className="fixed inset-0 overflow-hidden">
                    <div className="absolute inset-0 overflow-hidden">
                        <div className="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10">
                            <TransitionChild
                                enter="transform transition ease-in-out duration-500 sm:duration-700"
                                enterFrom="translate-x-full"
                                enterTo="translate-x-0"
                                leave="transform transition ease-in-out duration-500 sm:duration-700"
                                leaveFrom="translate-x-0"
                                leaveTo="translate-x-full"
                            >
                                <DialogPanel className="pointer-events-auto w-screen max-w-md">
                                    <div className="flex h-full flex-col overflow-y-scroll bg-white shadow-xl">
                                        <div className="flex-1 overflow-y-auto px-4 py-6 sm:px-6">
                                            <div className="flex items-start justify-between">
                                                <DialogTitle className="text-lg font-medium text-gray-900">Carrinho</DialogTitle>
                                                <div className="ml-3 flex h-7 items-center">
                                                    <button
                                                        type="button"
                                                        className="relative -m-2 p-2 text-gray-400 hover:text-gray-500"
                                                        onClick={ () => setOpen(false), handleContinueShopping }
                                                    >
                                                        <span className="absolute -inset-0.5" />
                                                        <span className="sr-only">Fechar</span>
                                                        <XMarkIcon className="h-6 w-6" aria-hidden="true" />
                                                    </button>
                                                </div>
                                            </div>

                                            <div className="mt-8">
                                                <div className="flow-root">
                                                    <ul role="list" className="-my-6 divide-y divide-gray-200">
                                                        {products.map((product) => (
                                                            <li key={product.prd_id} className="flex py-6">
                                                                <div className="h-24 w-24 flex-shrink-0 overflow-hidden rounded-md border border-gray-200">
                                                                    <img
                                                                        src={`data:image/jpeg;base64,${product.prd_imgProduto}`}
                                                                        alt={product.prd_descricao}
                                                                        className="h-full w-full object-cover object-center"
                                                                    />
                                                                </div>

                                                                <div className="ml-4 flex flex-1 flex-col">
                                                                    <div>
                                                                        <div className="flex justify-between text-base font-medium text-gray-900">
                                                                            <h3>
                                                                                <a href={product.href}>{product.prd_descricao}</a>
                                                                            </h3>
                                                                            <p className="ml-4">R${product.prd_valor}</p>
                                                                        </div>
                                                                    </div>
                                                                    <div className="flex flex-1 items-end justify-between text-sm">
                                                                        <p className="text-gray-500">Qtd {product.prd_quantidadeEstoque}</p>
                                                                        <input value={formData.Ped_Quantidade} onChange={handleChange} className="w-10 mr-20 2xl-shadow text-black focus:border-solid focus:border-[1px]border-[#035ec5] placeholder:text-gray bg-gray-200" type="number" id="Ped_Quantidade" name="Ped_Quantidade" />

                                                                        <div className="flex">
                                                                            <button
                                                                                type="button"
                                                                                className="font-medium text-indigo-600 hover:text-indigo-500"
                                                                                onClick={handleButtonClick}
                                                                                value={product.prd_id}
                                                                            >
                                                                                Remover
                                                                            </button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </li>
                                                        ))}
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                        <div className="border-t border-gray-200 px-4 py-6 sm:px-6">
                                            <div className="flex justify-between text-base font-medium text-gray-900">
                                                <p>Subtotal</p>
                                                <p>R${CalcSubTotal()}</p>
                                            </div>
                                            <p className="mt-0.5 text-sm text-gray-500">Envio e impostos calculados na finaliza&ccedil;&atilde;o da compra.</p>
                                            <div className="flex items-center justify-center mt-6">
                                                <button
                                                    href="#"
                                                    className="flex items-center justify-center rounded-md border border-transparent bg-indigo-600 px-6 py-3 text-base font-medium text-white shadow-sm hover:bg-indigo-700"
                                                    onClick={handleSubmit}
                                                >
                                                    Fazer pedido
                                                </button>
                                            </div>
                                            <div className="mt-6 flex justify-center text-center text-sm text-gray-500">
                                                <p>
                                                    or{' '}
                                                    <button
                                                        type="button"
                                                        className="font-medium text-indigo-600 hover:text-indigo-500"
                                                        onClick={ () => setOpen(false), handleContinueShopping }
                                                    >
                                                        Continuar comprando
                                                        <span aria-hidden="true"> &rarr;</span>
                                                    </button>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </DialogPanel>
                            </TransitionChild>
                        </div>
                    </div>
                </div>
            </Dialog>
        </Transition>
    )
}
