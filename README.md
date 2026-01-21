Para o avaliador

M<inha visão sobre oque foi codado ate agora (Sales Core)
o foco foi adicinar a funcionalidade de Vendas usando ddd e cqrs.

1. Dominio e Regras de Negocio
Entidades Sale e SaleItem: criei o core das vendas. a entidade nao é so um objeto de dados, ela tem "cerebro".

Motor de Desconto: a regra de desconto progressivo (10% e 20%) ta direto no dominio. se mudar a qtd, o calculo trigga sozinho.

Status de Venda: decidi não utilizar booleano simples e coloquei um Enum SaleStatus. agora a venda tem ciclo de vida: Pending, Completed, Cancelled.

2. Arquitetura e CQRS com MediatR
CreateSale: o handler recebe o request, mapeia pro command e ja faz o save no postgres.

GetSale: busca rapida por ID devolvendo um DTO limpo pro front.

UpdateSale: implementei a edicao completa. se o usuário mudar os itens, a api recalcula o total da sale e os descontos de novo na hora do save.

CancelSale: aqui nao tem DELETE fisico no banco nao, fiz o cancelamento logico (soft delete) pra nao perder o historico da operacao.

3. Infra e Persistencia
SaleRepository: criei o repo pra abstrair o EF Core. as queries estao todas async pra nao travar a thread.

Mapeamento das Tables: configurei o OnModelCreating pra garantir que o postgres crie as tabelas Sales e SaleItems com os relacionamentos correto de FK.

Migrations no Startup: o app agora tenta rodar o context.Database.Migrate() quando sobe o container. se tiver migration nova no codigo, ele ja atualiza o banco sozinho.

4. Setup de Dev e Tooling
AutoMapper Profiles: criamos os profiles pra traduzir oque vem da webapi pros commands da application sem precisar de "de-para" manual.

Debug Hibrido: configurei o docker-compose pra deixar a infra (db, redis, mongo) no container mas deixar o dev rodar a api local no visual studio ou vscode pra conseguir debugar e botar breakpoint.

Global Error Handling: se der ruim na validacao do fluentvalidation, o middleware ja pega o erro e devolve um json padrao para o front.
