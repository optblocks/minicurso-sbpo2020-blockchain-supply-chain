# minicurso-sbpo2020-blockchain-supply-chain


## Readme for invoking the SupplyChain NFT contract

```cs
52eaab8b2aab608902c651912db34de36e7a2b0f = 0f2b7a6ee34db32d9151c6028960ab2a8babea52 = APLJBPhtRg2XLhtpxEHd6aRNL7YSLGH2ZL
[{"type":7,"value":"mintNFT"},{"type":16,"value":[{"type":7,"value":"LAND1"},{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"},{"type":7,"value":"Terra para plantio Caparao"}]}]

[{"type":7,"value":"mintNFT"},{"type":16,"value":[{"type":7,"value":"LAND2"},{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"},{"type":7,"value":"Terra para plantio Diamantina"}]}]

[{"type":7,"value":"mintNFT"},{"type":16,"value":[{"type":7,"value":"LAND3"},{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"},{"type":7,"value":"Terra para plantio Sul de Minas"}]}]

b1761b44255107162d3c79680b81b4c8a5efc597 = 97c5efa5c8b4810b68793c2d16075125441b76b1 = AXxCjds5Fxy7VSrriDMbCrSRTxpRdvmLtx
[{"type":7,"value":"mintNFT"},{"type":16,"value":[{"type":7,"value":"TRUCK1"},{"type":5,"value":"b1761b44255107162d3c79680b81b4c8a5efc597"},{"type":7,"value":"Caminhao para transporte certifacado"}]}]

[{"type":7,"value":"mintNFT"},{"type":16,"value":[{"type":7,"value":"TRUCK1"},{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"},{"type":7,"value":"Caminhao para transporte certifacado II"}]}]


Resultado sera 100000000
[{"type":7,"value":"balanceOf"},{"type":16,"value":[{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"},{"type":7,"value":"TRUCK1"}]}]

Resultado sera 100000000
[{"type":7,"value":"balanceOf"},{"type":16,"value":[{"type":5,"value":"b1761b44255107162d3c79680b81b4c8a5efc597"},{"type":7,"value":"TRUCK1"}]}]

Resultado sera 100000000
[{"type":7,"value":"balanceOf"},{"type":16,"value":[{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"},{"type":7,"value":"LAND1"}]}]

Resultado sera 0
[{"type":7,"value":"balanceOf"},{"type":16,"value":[{"type":5,"value":"b1761b44255107162d3c79680b81b4c8a5efc597"},{"type":7,"value":"LAND1"}]}]

Chama por  AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y - Resultado Nada, não passa verificação
[{"type":7,"value":"notifica"},{"type":16,"value":[{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"}, {"type":7,"value":"TRUCK1"}, {"type":7,"value":"Update TRUCK1 Allowed"}]}]

Chama por  AXxCjds5Fxy7VSrriDMbCrSRTxpRdvmLtx - Resultado Nada, não passa verificação ainda, pois nao podemos notificar um token do owner APLJBPhtRg2XLhtpxEHd6aRNL7YSLGH2ZL com AXxCjds5Fxy7VSrriDMbCrSRTxpRdvmLtx
[{"type":7,"value":"notifica"},{"type":16,"value":[{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"}, {"type":7,"value":"TRUCK1"}, {"type":7,"value":"Update TRUCK1 Allowed"}]}]

Chama por  APLJBPhtRg2XLhtpxEHd6aRNL7YSLGH2ZL 
Token not found for this owner
[{"type":7,"value":"notifica"},{"type":16,"value":[{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"}, {"type":7,"value":"LAND11"}, {"type":7,"value":"Update TRUCK1 Allowed"}]}]

Chama por  APLJBPhtRg2XLhtpxEHd6aRNL7YSLGH2ZL 
Notificação status ok
[{"type":7,"value":"notifica"},{"type":16,"value":[{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"}, {"type":7,"value":"LAND1"}, {"type":7,"value":"Update TRUCK1 Allowed"}]}]
Aqui nao avançamos na lógica, em criar uma lista que diz quais caminhões são permitidos (dentro do smart contract)
Da maneira que está o exemplo, essa verificação é feita pelo fornecedor.
Porem, conforme visto, toda operação segue certificada.

Chama por AXxCjds5Fxy7VSrriDMbCrSRTxpRdvmLtx 
[{"type":7,"value":"notifica"},{"type":16,"value":[{"type":5,"value":"b1761b44255107162d3c79680b81b4c8a5efc597"}, {"type":7,"value":"TRUCK1"}, {"type":7,"value":"LAND1 100KG CAFÉ DO BOM EM COLETA"}]}]

Chama por AXxCjds5Fxy7VSrriDMbCrSRTxpRdvmLtx 
[{"type":7,"value":"notifica"},{"type":16,"value":[{"type":5,"value":"b1761b44255107162d3c79680b81b4c8a5efc597"}, {"type":7,"value":"TRUCK1"}, {"type":7,"value":"LAND2 80KG CAFÉ DO BOM EM COLETA"}]}]

Chama por  AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y 
[{"type":7,"value":"mintNFT"},{"type":16,"value":[{"type":7,"value":"DIST1"},{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"},{"type":7,"value":"ArmazemGourmet"}]}]

Chama por  APLJBPhtRg2XLhtpxEHd6aRNL7YSLGH2ZL 
[{"type":7,"value":"notifica"},{"type":16,"value":[{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"}, {"type":7,"value":"DIST1"}, {"type":7,"value":"TRUCK1 CARGA RECEBIDA"}]}]
TX ID a04a4e31295ab10a8fde4d2cd3264530c584d5d779b823baaf7c83c0866f0bd1

Chama por  AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y 
[{"type":7,"value":"mintNFT"},{"type":16,"value":[{"type":7,"value":"CLI1"},{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"},{"type":7,"value":"Cliente com referencia interna X"}]}]

Chama por  APLJBPhtRg2XLhtpxEHd6aRNL7YSLGH2ZL
Attach 1 NEO pago ao Dono do contrato (Pelo Café)
[{"type":7,"value":"notifica"},{"type":16,"value":[{"type":5,"value":"52eaab8b2aab608902c651912db34de36e7a2b0f"}, {"type":7,"value":"DIST1"}, {"type":7,"value":"CAFE PAGO 10 gramas expresso a04a4e31295ab10a8fde4d2cd3264530c584d5d779b823baaf7c83c0866f0bd1 "}]}]

```
