import React, { useState, useEffect } from 'react'
import PropTypes from 'prop-types';
import Box from '@mui/material/Box';
import Collapse from '@mui/material/Collapse';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import api from '../../../api';

function Row(props) {
    const { row } = props;
    const [open, setOpen] = React.useState(false);

    return (
        <React.Fragment>
            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
                <TableCell component="th" scope="row">
                    {row.ped_id}
                </TableCell>
                <TableCell align="center">{row.ped_Id}</TableCell>
                <TableCell align="center">{row.prd_Id}</TableCell>
                <TableCell align="center">{row.usu_Id}</TableCell>
                <TableCell align="center">{row.end_Id}</TableCell>
                <TableCell align="center">{row.ped_Quantidade}</TableCell>
                <TableCell align="center">{row.ped_FormaPagamento}</TableCell>
                <TableCell align="center">{row.ped_Situacao}</TableCell>
                <TableCell align="center">{row.ped_DataPedido}</TableCell>
            </TableRow>
        </React.Fragment>
    );
}

export default function TablePedidos() {
    const [rows, setRows] = useState([]);

    const fetchPedidos = async () => {
        try {
            const response = await api.get(`/Pedido/SelecionarTodos?pageNumber=1&pageSize=50`);
            setRows(response.data);
        } catch (error) {
            console.error('Erro ao buscar pedidos:', error);
        }
    };

    useEffect(() => {
        fetchPedidos();
    }, []);

    return (
        <TableContainer component={Paper}>
            <Table aria-label="collapsible table">
                <TableHead>
                    <TableRow>
                        <TableCell />
                        <TableCell align="center">Pedido</TableCell>
                        <TableCell align="center">Produto</TableCell>
                        <TableCell align="center">Usu&aacute;rio</TableCell>
                        <TableCell align="center">Endere&ccedil;o</TableCell>
                        <TableCell align="center">Quantidade</TableCell>
                        <TableCell align="center">Forma de pagamento</TableCell>
                        <TableCell align="center">Situa&ccedil;&atilde;o</TableCell>
                        <TableCell align="center">Data do pedido</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <Row key={row.ped_id} row={row} />
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}
