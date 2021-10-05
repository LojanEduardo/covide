# covide
Covid-e é um software desenvolvido em MVC, sendo uma ferramenta com intuito de administrar vacinas, lotes de vacinas e vacinados do covid-19, cadastrados no sistema.
------------------
Primeiramente para abrir o software é necessário abri o arquivo Covid.sln

Após isso para conseguir usar essa aplicação é necessario colocar sua senha do Banco no último web.config                                
                                                                                                                              Colocar a senha aqui
                                                                                                                                     |
  <connectionStrings>                                                                                                                V
    <add name="StringConexao" providerName="MySql.Data.MySqlClient" connectionString="server=localhost;database=groupus;uid=root;pwd=''" />
  </connectionStrings>

Eu deixei o Bd.sql na Brench Master e com ele você é capaz de criar o banco, com alguns pré cadastros, como um usuário com o login "liax@gmail.com" e senha "Abc123".
