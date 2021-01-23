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



#### Criando o projeto
```
dotnet new mvc --auth Individual -uld -o PastaProjeto
```

Obs: '-uld' define o tipo de BD como Sql Server, já adicionando os pacotes necessários. 


#### Pacote de Edição de Arquivos do Identity

```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer 
```

#### Exibir arquivos do Identity:

Instalar pacote:
```
dotnet tool install -g dotnet-aspnet-codegenerator
```

###### Exibe arquivos básicos:

```
dotnet aspnet-codegenerator identity -dc Projeto.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout"
```

###### Exibe todos os arquivos:

```
dotnet aspnet-codegenerator identity -dc Projeto.Data.ApplicationDbContext"
```


#### Desabilitar confirmação de Email:
```
public void ConfigureServices(IServiceCollection services)
{
	services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = <<<<false>>>>)
		.AddEntityFrameworkStores<ApplicationDbContext>();ntext 
}
```

#### Configuração de Padrão de Senha / Password:
```
services.Configure<IdentityOptions>(options =>
{	
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;

	// Lockout settings.
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
	options.Lockout.MaxFailedAccessAttempts = 5;
	options.Lockout.AllowedForNewUsers = true;

	// User settings.
	options.User.AllowedUserNameCharacters =
	"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	options.User.RequireUniqueEmail = false;
});
```

## Two Factor Authentication 

Exibir as dependências do Identity com o seguinte cómando::

```
dotnet aspnet-codegenerator identity -dc Projeto.Data.ApplicationDbContext 
```

Baixe e descompacte [esse arquivo](https://davidshimjs.github.io/qrcodejs/), adicionando-o no diretório 'wwwroot/lib' do seu projeto.

Acrescentar o codigo abaixo em no diretório '/areas/Identity/pages/Account/Manage/EnableAuthenticator.cshtml' no seu projeto:

```
@section Scripts {
	@await Html.PartialAsync("_ValidationScriptsPartial")
 	<script type="text/javascript" src="~/lib/qrcode.js"></script>
 	<script>
		text: "@Html.Raw(Model.AuthenticatorUri)", 
			width: 150,
			new QRCode(document.getElementById("qrCode"),
			{
				height: 150
			});
	</script>
}
```

Pronto, agora o próprio usuário pode ir nas configurações e habilitar a autenticação em dois fatores. Lembrando que ele irá precisar baixar algum app two factor authentication. Os mais comuns são o [Microsoft Authenticator](https://play.google.com/store/apps/details?id=com.azure.authenticator&hl=en) e o [Google Authenticator](https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2&hl=en).

Com o app instalado o usuário pode ir em gerenciar seus dados e clicar em Two Factor… / autenticação em dois fatores, apontar com o smartphone ao qrcode e em seguida digitar o codigo gerado pelo app no campo. 

A aplicação irá configurar o restante e responder com alguns códigos de recuperação, que o usuário precisa guardar. 

A qualquer momento o proprio usuário pode habilitar ou desabilitar esse tipo de autenticação. 

A partir da próxima vez que for fazer o login, a aplicação irá pedir o código de autorização que o app baixado irá gerar. 


##  Login External

#### Facebook

Adicione o pacote [Microsoft.AspNetCore.Authentication.Facebook](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.Facebook) ao projeto.

Navegue até as [configurações do facebook](https://developers.facebook.com/apps/) para desenvolvedores.

Adicione um produto tipo login facebook.

Adicione em URIs de redirecionamento OAuth válido (a url da sua aplicação), exemplo: https://localhost:44320/signin-facebook 

Clique em salvar

Vá em ‘Configurações’ => ‘Basico’ e pegue o ID do Aplicativo e a chave secret.

Habilite o armazenamento secreto no projeto com o seguinte comando:
```
dotnet user-secrets init
```

Os seguintes comandos inserem o ID do App e a chave secreta do Facebook no projeto:
```
dotnet user-secrets set "Authentication:Facebook:AppId" "<app-id>"
dotnet user-secrets set "Authentication:Facebook:AppSecret" "<app-secret>"
```

Obs: substitua <...> pelos valores descritos no item 6


Adicione o seguinte codigo em Startup.cs => ConfigureServices:
```
services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
});
```

Pronto, a opção de login com Facebook está disponível
	
