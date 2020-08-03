import React, { useState, useEffect } from 'react'
import Layout from '../Layout'
import MaterialTable, { MTableToolbar } from 'material-table'
import Button from '@material-ui/core/Button'
import { Link } from 'react-router-dom'
import { formatarValor } from '../../utils'
import { CSVLink } from 'react-csv'
import styled from 'styled-components'
import { DatePicker } from '@material-ui/pickers'
import moment, { Moment } from 'moment'
import { Paper, Typography } from '@material-ui/core'
import ExcelSvg from '../../assets/icons8-microsoft-excel-2019.svg'
interface Pedido {
    id: number,
    nome: string,
    quantidade: number,
    valor: number,
    taxaEntrega: number,
    tipo: string,
    origem: string,
    meioPagamento: string,
    dataCriacao: Date
}

const csvHeaders = [
    { label: 'Id do Pedido', key: 'pedidoId' },
    { label: 'Data do Pedido', key: 'data' },
    { label: 'Origem', key: 'origem' },
    { label: 'Tipo do pedido', key: 'tipo' },
    { label: 'Meio de pagamento', key: 'meioDePagamento' },
    { label: 'Id do Produto', key: 'produtoId' },
    { label: 'Categoria do Produto', key: 'categoria' },
    { label: 'Nome do Produto', key: 'nomeProduto' },
    { label: 'Id do adicional', key: 'adicionalId' },
    { label: 'Tipo do adicional', key: 'tipoAdicional' },
    { label: 'Nome do adicional', key: 'adicionalNome' },
    { label: 'Valor', key: 'valor' },
]

const BotãoBaixarRelatório = styled(CSVLink)`
    color: rgba(0, 0, 0, 0.87);
    padding: 6px 16px;
    font-size: 0.875rem;
    min-width: 64px;
    box-sizing: border-box;
    font-weight: 500;
    line-height: 1.75;
    border-radius: 4px;
    text-transform: uppercase;
    text-decoration: none;
    background-color: #1f8b57;
    color: white;
    
    position: relative;
    display: flex;
    height: 35px;
    width: 150px;
    padding: 5px;
    align-items: center;

    &:hover {
        background-color: #10613a;
        transition: background-color 0.2s ease-in;
    }
`

const Icone = styled.img`
    position:relative;
    height: 90%;
    width: auto;
    margin: 2px 10px;
`

const ActionBar = styled(Paper)`
    display: flex;
    align-items: center;
    justify-content: space-around;
    padding: 10px;
    margin-bottom: 10px;
`

const CampoData = styled(DatePicker)`
  & .MuiOutlinedInput-input {
        padding: 10px;
  }
`

const TableToolbar = styled.div`
    display:flex;
    justify-content: space-between;
    align-items: center;
    flex: 1;

    & .MuiToolbar-root {
        flex: 1;
    }
`

const ValorTotal = styled(Typography)`
    align-content: flex-end;
    justify-content: flex-end;
    align-items: flex-end;
    display: flex;
    padding-right: 24px;
`

const PaginaPedidos = ({ history }: any) => {
    const [pedidos, setPedidos] = useState([])
    const [pedidosDownload, setPedidosDownload] = useState([])
    const [relatórioGerado, setRelatórioGerado] = useState(true)
    const [loading, setLoading] = useState(false)
    const [dataSelecionada, setDataSelecionada] = useState<Moment | null>(moment())

    useEffect(() => {
        setRelatórioGerado(false)
        setLoading(true)

        fetch(`/api/pedidos?data=${dataSelecionada?.toISOString()}`)
            .then(res => res.json())
            .then(data => setPedidos(data))
            .then(() => setLoading(false))

        fetch(`/api/pedido/download/${dataSelecionada?.toISOString()}`)
            .then(res => res.json())
            .then(data => setPedidosDownload(data))
            .then(() => setRelatórioGerado(true))

    }, [dataSelecionada])

    return <Layout pagename="Pedidos">
        <ActionBar>
            <Button component={Link} variant="contained" to="/pedidos/criar" color="primary">Novo pedido</Button>

            <CampoData value={dataSelecionada}
                inputVariant="outlined"
                variant="inline"
                format="DD/MM/yyyy"
                label="Filtrar por dia:"
                autoOk
                onChange={(date) => setDataSelecionada(date)} />

            {!relatórioGerado && 'Carregando relatório'}
            {
                relatórioGerado &&
                <BotãoBaixarRelatório
                    className="MuiButton-contained"
                    data={pedidosDownload}
                    target="_blank"
                    headers={csvHeaders}
                    filename={`relatorio-${new Date().toISOString()}.csv`}
                    onClick={() => setRelatórioGerado(true)}
                ><Icone src={ExcelSvg} /> Baixar</BotãoBaixarRelatório>
            }
        </ActionBar>

        <MaterialTable
            columns={[
                { title: 'ID', field: 'id' },
                { title: 'Data de criação', field: 'dataCriacao' },
                { title: 'Meio de pagamento', field: 'meioPagamento' },
                { title: 'Origem', field: 'origem' },
                { title: 'Tipo', field: 'tipo' },
                { title: 'Quantidade de itens', field: 'quantidade' },
                { title: 'Taxa de entrega', field: 'taxaEntrega' },
                { title: 'Valor', field: 'valor' },
            ]}
            data={pedidos.map((p: Pedido) => ({ ...p, valor: formatarValor(p.valor), taxaEntrega: formatarValor(p.taxaEntrega), dataCriacao: moment(p.dataCriacao).format('DD/MM/YYYY hh:mm:ss') }))}
            title="Lista de pedidos"
            options={{ search: false, pageSize: 10 }}
            isLoading={loading}
            components={{
                // eslint-disable-next-line react/display-name
                Toolbar: props => (
                    <TableToolbar>
                        <MTableToolbar {...props} />
                        <ValorTotal variant="h6" >
                            Total: {formatarValor(pedidos.map((x: Pedido) => x.valor).reduce((acc, a) => acc + a, 0))}
                        </ValorTotal>
                    </TableToolbar>
                ),
            }}
            onRowClick={(event, rowData) => history.push(`pedido/${rowData?.id}`)}

        />

    </Layout>
}


export default PaginaPedidos