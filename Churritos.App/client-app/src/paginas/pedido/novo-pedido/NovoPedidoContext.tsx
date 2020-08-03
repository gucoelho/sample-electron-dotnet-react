import React, {createContext, useState} from 'react'
import {Cliente, Endereco} from '../Models'

interface NovoPedido {
  cliente: Cliente
  endereco: Endereco
  setCliente: (cliente: Cliente) => void
  setEndereco: (endereco: Endereco) => void
}

const defaultCliente : Cliente = { nome: '', cpf: '', telefone: '' }

const defaultEndereco : Endereco = {
    logradouro: '',
    complemento: '',
    bairro: '',
    cidade: '',
    estado: 'SP',
    observacao: ''
}

export const NovoPedidoContext = createContext<NovoPedido>({
    cliente: defaultCliente,
    endereco: defaultEndereco,
    setCliente: () => null,
    setEndereco: () => null
})

export const NovoPedidoContextProvider = (props : any) => {
    const [cliente, setCliente] = useState<Cliente>(defaultCliente)
    const [endereco, setEndereco] = useState<Endereco>(defaultEndereco)

    return <NovoPedidoContext.Provider value={{ cliente, endereco, setCliente, setEndereco }}>
        {props.children}
    </NovoPedidoContext.Provider>
}