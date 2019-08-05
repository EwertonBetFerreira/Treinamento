﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treinamento._2___MODEL;
using Treinamento._3___DAO;

namespace Treinamento._1___VIEW
{
    public class ViewTransferencia
    {
        public void RealizaTransferencia(ViewContaBancaria viewConta, Operacao operacao, RelatorioOperacaoDao relatorioDao, ContaBancariaDao contaDao)
        {
            Console.Clear();

            Console.WriteLine("Informe o ID da sua conta bancaria");
            int IdContaOrigem = Convert.ToInt32(Console.ReadLine());

            ContaBancaria contaOrigem = contaDao.BuscaContaPorId(IdContaOrigem);

            if (contaOrigem != null)
            {
                Console.WriteLine("\nConfirme os dados: \n");
                viewConta.FormataListaContaBancaria(contaDao);

                Console.WriteLine("\nInforme o ID da conta bancaria que vai receber a transferencia");
                int IdContaDestino = Convert.ToInt32(Console.ReadLine());

                ContaBancaria contaMovimentada = contaDao.BuscaContaPorId(IdContaDestino);

                if (contaMovimentada != null)
                {
                    Console.WriteLine("\nConfirme os dados: \n");
                    viewConta.FormataListaContaBancaria(contaDao);

                    Console.WriteLine("\nInforme o valor a ser transferido: ");
                    double ValorTransferencia = Convert.ToDouble(Console.ReadLine());

                    if (contaOrigem.Saldo < ValorTransferencia)
                    {
                        Console.WriteLine("Voce não tem saldo suficiente para fazer este depósito");
                    }
                    else
                    {
                        contaOrigem.Saque(ValorTransferencia);
                        contaMovimentada.Deposito(ValorTransferencia);

                        operacao.RealizaOperacao(contaMovimentada, contaOrigem, 3);

                        relatorioDao.AdicionaNovaOperacao(operacao);

                        Console.WriteLine("Operacao realizada com sucesso");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhuma conta encontrada");
                }

            }
            else
            {
                Console.WriteLine("Nenhuma conta encontrada");
            }
        }
    }
}
