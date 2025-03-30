markdown
Copy
# Toro Investimentos Dashboard (Angular)

Dashboard para gestão de investimentos desenvolvido em Angular 16.

---

## 📋 Pré-requisitos

### Para o Frontend (Angular 16)
- **Node.js 16.x ou superior**  
  [Download Node.js](https://nodejs.org/)
- **npm 8.x ou superior** (vem com o Node.js)
- **Angular CLI 16**  
  Instale globalmente com:  
  ```bash
  npm install -g @angular/cli
🛠️ Instalação do Projeto
1. Clonar o Repositório
bash
Copy
git clone [URL_DO_SEU_REPOSITÓRIO]
cd toroinvestimentos-dashboard-angular
2. Instalar Dependências
bash
Copy
npm install
3. Configuração Inicial
Variáveis de ambiente:
Edite src/environments/environment.ts conforme necessário:

typescript
Copy
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000' // URL da API
};
🚀 Comandos Úteis
Comando	Descrição
ng serve	Inicia servidor de desenvolvimento (4200)
ng build --configuration production	Gera build de produção (pasta dist/)
ng test	Executa testes unitários via Karma
ng e2e	Executa testes end-to-end via Protractor
ng generate component meu-componente	Cria novo componente
🏗️ Estrutura do Projeto (Angular)
plaintext
Copy
toroinvestimentos-dashboard-angular
├── src/
│   ├── app/
│   │   ├── app.module.ts            # Módulo principal
│   │   ├── app.routing.ts           # Configuração de rotas
│   │   ├── home/                    # Componente Home
│   │   │   ├── home.component.ts    # Lógica do componente
│   │   │   ├── home.component.html  # Template
│   │   │   └── home.component.css   # Estilos específicos
│   ├── assets/                      # Imagens/fontes
│   └── environments/                # Configurações de ambiente
├── angular.json                     # Configuração do workspace
├── package.json                     # Dependências e scripts
🔧 Configurações Avançadas
Proxy para API
Crie proxy.conf.json na raiz:

json
Copy
{
  "/api": {
    "target": "http://localhost:5000",
    "secure": false,
    "logLevel": "debug"
  }
}
Execute com proxy:

bash
Copy
ng serve --proxy-config proxy.conf.json
Build de Produção
bash
Copy
ng build --configuration production
Arquivos otimizados serão gerados na pasta dist/

🧪 Testes
Testes Unitários (Karma)
bash
Copy
ng test
Relatório interativo no navegador em http://localhost:9876

Testes End-to-End (Protractor)
bash
Copy
ng e2e
Requer servidor em execução (ng serve)

🚨 Troubleshooting
Problemas Comuns
Erro de versão do Angular CLI

Garanta a versão correta:

bash
Copy
ng version
Atualize se necessário:

bash
Copy
npm update -g @angular/cli
Falha na instalação de pacotes

Limpe cache e reinstale:

bash
Copy
npm cache clean --force
rm -rf node_modules package-lock.json
npm install
Erros de CORS

Configure o proxy ou ajuste o CORS no backend:

bash
Copy
ng serve --proxy-config proxy.conf.json
