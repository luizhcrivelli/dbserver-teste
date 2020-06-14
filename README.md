# DBServer , Teste para Super Digital

Caso deseje iniciar a aplicação diretamente pela SD.API, inseri o swagger na inicialização já  para efetuar o teste manual. 
http://localhost:50001/swagger/
Nesta tela vc já encontrará o json que o post espera receber e os httpcodes de retornos esperados ( 200 e 400 )
Para o tipo de operação tem um enum onde é necessário ser enviado 1 para credito e 2 para débito.

Todos recursos solicitados no teste estão aplicados.

Caminho da chamada da api : http://localhost:50001/Conta/EfetuarOperacao

Para os testes, só dar um Run Tests pelo proprio VS nos projetos que estão na pasta tests.

Print dos resultados do teste:
![Alt text](/resultadoDoTeste.png?raw=true "Testes")

Utilizado:
Visual Studio 2019 , .net core 2.1 , xunit , swagger , FluentAssertions

Print do Swagger esperado:
![Alt text](/screenSwagger.png?raw=true "Testes")
