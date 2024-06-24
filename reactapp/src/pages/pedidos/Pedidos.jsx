import * as React from "react";
import TablePedidos from "../../ui/components/table/TablePedidos";

const Pedidos = () => {
    return (
        <div className="h-full grid">
            <div className="py-16 px-64">
                <h1 className="text-3xl font-bold hover:underline">Seus pedidos</h1>
            </div>
            <div className="px-64 pb-10">
                <input
                    placeholder="Procurar produto..."
                    class="text-white border-2 rounded-[10px] py-[10px] px-[25px] bg-transparent max-w-[190px] focus:outline-none focus:shadow-inner focus:shadow-[2px_2px_15px_#8707ff]"
                    name="text"
                    type="text"
                />
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