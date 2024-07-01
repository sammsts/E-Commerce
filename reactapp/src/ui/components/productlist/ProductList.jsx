import React, { useEffect, useState } from 'react';
import api from '../../../api';

export default function ProductList(data) {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await api.get('/Produto/SelecionarTodos?PageNumber=1&pageSize=50');
                setProducts(response.data);
            } catch (error) {
                console.error('Erro ao buscar produtos:', error);
            }
        };

        fetchProducts();
    }, []);

    const filteredProducts = products.filter(product => product.prd_serialId && product.prd_serialId.toString().startsWith(data.category ? data.category : ''));

    return (
        <div className="bg-white">
            <div className="mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
                <h2 className="text-2xl font-bold tracking-tight text-gray-900">Clique para visualizar o produto</h2>

                <div className="mt-6 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
                    {filteredProducts.map((product) => (
                        <div key={product.prd_serialId} className="group relative">
                            <div className="aspect-h-1 aspect-w-1 w-full overflow-hidden rounded-md bg-gray-200 lg:aspect-none group-hover:opacity-75 lg:h-80">
                                <img src={`data:image/jpeg;base64,${product.prd_imgProduto}`}
                                    alt={product.prd_descricao}
                                    className="h-full w-full object-cover object-center lg:h-full lg:w-full"
                                />
                            </div>
                            <div className="mt-4 flex justify-between">
                                <div>
                                    <h3 className="text-sm text-gray-700">
                                        <a href={`/productoverview?serial=${product.prd_serialId}`}>
                                            <span aria-hidden="true" className="absolute inset-0" />
                                            {product.prd_descricao}
                                        </a>
                                    </h3>
                                    <p className="mt-1 text-sm text-gray-500">Estoque: {product.prd_quantidadeEstoque}</p>
                                </div>
                                <p className="text-sm font-medium text-gray-900">R${product.prd_valor}</p>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    )
}
