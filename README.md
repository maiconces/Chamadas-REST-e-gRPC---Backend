<p><center><img src="https://assets.website-files.com/5cf95301995e8c48a8880a69/5ec524104ef2ed3e912b5348_COLORIDA-p-500.png"></center></p>
<br>

# Chamadas-REST-e-gRPC---Backend
Descrição: Como a base deste trabalho é a comparação da performance de duas tecnologias para com, foi necessário criar duas aplicações como experimento, uma usando REST/JSON e outra gRPC/Protobuf como meio de comunição em uma rede de microsserviços. E só assim través o uso de ferramentas de monitoramento e vizualização de dados foi possivel realizar uma análise e comparar o desempenho com base nos resultados obtidos, pode-se afirmar que a tecnologia REST funciona bem com a interface JSON para trafegar informações entre uma rede de microserviços. Porém o gRPC juntamente com Protobuf, como observado neste trabalho provou ser mais performático no que diz respeito ao tempo de resposta da transferência de dados entre os serviços, quanto a carga máxima de dados transferidos por minuto, além de obter o menor tempo de latência na comunicação entre as requisições enviadas pelo microsserviço. 
<br/>

Aluno: Maicon Alcântara de Oliveira <br/>
Professor Orientador: Romualdo Monteiro de Resende Costa <br/>
TCC: Análise da Eficiência da Transferência de Dados em uma Rede de Microserviços – Proposta de Comparação de Desempenho entre REST E GRPC. <br>
Ano: 2022-2.

Link: https://seer.uniacademia.edu.br/index.php/engsoftware/index

#### Depêndencias necessárias
* Prometheus - https://prometheus.io/docs/introduction/overview/
* Grafana - https://grafana.com/grafana/download
* .NetCore - V3.1 - https://dotnet.microsoft.com/en-us/download/dotnet-framework
* SQLServer - https://www.microsoft.com/pt-br/sql-server/sql-server-downloads

#### Depêndencias para desenvolvimento
 * .NetCore SDK - v3.1+ - https://dotnet.microsoft.com/en-us/download/dotnet
 * Visual Studio 2022 (Windows e MAC) - https://visualstudio.microsoft.com/downloads
 * Visual Studio Code (Linux) - https://code.visualstudio.com/docs?dv=win&wt.mc_id=DX_841432&sku=codewin
 
### Informações importantes
#### Como instalar ferramentas do EntityFrameworkCore
 * [learn.microsoft.com/pt-br/ef/core/cli/powershell](https://learn.microsoft.com/pt-br/ef/core/cli/powershell)
#### Como gerenciar migrações usando o EF
 * https://learn.microsoft.com/pt-br/ef/core/managing-schemas/migrations/managing?tabs=dotnet-core-cli
