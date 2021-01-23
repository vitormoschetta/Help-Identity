# Identity

## Tabelas e Conceitos

1. Tabela AspNetUsers  
   - 'SecurityStamp / Selo de Segurança'
   	=> valor muda ao trocar senha e/ou perfil. serve p invalidar coockies antigos.
   
   - ConcurrencyStamp / Selo de simultaneidade
	=> para controlar quando um mesmo dado está sendo editado por diferentes usuários simultaneamente
	
   - LockoutEnabled / Bloqueio ativado
	=> informa se o bloqueio para muitas tentativas de login está ativado ou não.
	
   - LockoutEnd / Fim do Bloqueio
	=> informa a data até a qual o usuário ficará bloqueado 
		
2. Tabela AspNetRoles  
Armazena 'funções' / 'perfis', e tem um relacionamento de muitos-para-muitos com a tabela AspNetUsers,
por isso existe a tabela de ligação AspNetUserRoles. Ou seja, um usuário pode ter muitos perfis, e um
Perfil pode estar direcionado a muitos Usuários. Logo se trata de autorização para grupos de usuários.

3. Tabela AspNetUserClaims  
Uma Claim é um par de valores definidos para um único usuário. É uma relação de um-para-muitos. 
   
4. Tabelas AspNetUserTokens e AspNetUserLogins   
São utilizadas para armazenar webtokens e chaves de usuario de login externo



## Tutorial de Início:
<https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=netcore-cli>


## Tutorial para implementação de confirmação de E-mail com SendGrid:
<https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/accconfirm?view=aspnetcore-5.0&tabs=netcore-cli>


## Tutorial para configurar Logon Externo
<https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/social/?view=aspnetcore-5.0&tabs=visual-studio-code> 


## Tutorial para adicionar QR Code à pagina de autenticação por dois fatores
<https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity-enable-qrcodes?view=aspnetcore-5.0>