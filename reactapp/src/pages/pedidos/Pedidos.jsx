import * as React from "react";
import TablePedidos from "../../ui/components/table/TablePedidos";

const Pedidos = () => {
    return (
        <div className="h-full grid">
            <div className="py-16 px-64">
                <h1 className="text-3xl font-bold hover:underline">Seus pedidos</h1>
            </div>
            <div className="flex justify-center items-center">
                <div className="w-9/12 shadow-2xl shadow-neutral-950">
                    <TablePedidos />
                </div>
            </div>
        </div>
    );
};

export default Pedidos;