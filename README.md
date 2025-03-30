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
  ```

---

## 🛠️ Instalação do Projeto

1. **Clonar o Repositório**
   ```bash
   git clone [URL_DO_SEU_REPOSITÓRIO]
   cd toroinvestimentos-dashboard-angular
   ```

2. **Instalar Dependências**
   ```bash
   npm install
   ```

3. **Configuração Inicial**
   
   Variáveis de ambiente:  
   Edite `src/environments/environment.ts` conforme necessário:
   
   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'http://localhost:5000' // URL da API
   };
   ```

---

## 🚀 Comandos Úteis

| Comando                                    | Descrição                                            |
| ------------------------------------------ | ---------------------------------------------------- |
| `ng serve`                                 | Inicia servidor de desenvolvimento (porta 4200)      |
| `ng build --configuration production`      | Gera build de produção (pasta `dist/`)               |
| `ng test`                                  | Executa testes unitários via Karma                   |
| `ng e2e`                                   | Executa testes end-to-end via Protractor             |
| `ng generate component meu-componente`     | Cria novo componente                                 |

---

## 🏗️ Estrutura do Projeto (Angular)

```plaintext
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
```

---

## 🔧 Configurações Avançadas

### Proxy para API

Crie o arquivo `proxy.conf.json` na raiz do projeto com o seguinte conteúdo:

```json
{
  "/api": {
    "target": "http://localhost:5000",
    "secure": false,
    "logLevel": "debug"
  }
}
```

Para executar com o proxy, utilize o comando:

```bash
ng serve --proxy-config proxy.conf.json
```

### Build de Produção

Para gerar o build de produção, execute:

```bash
ng build --configuration production
```

Os arquivos otimizados serão gerados na pasta `dist/`.

---

## 🧬 Testes

### Testes Unitários (Karma)

Execute os testes unitários com:

```bash
ng test
```

Um relatório interativo será aberto no navegador em [http://localhost:9876](http://localhost:9876).

### Testes End-to-End (Protractor)

Para executar os testes end-to-end, use:

```bash
ng e2e
```

> **Nota:** Os testes end-to-end requerem que o servidor esteja em execução (`ng serve`).

---

## 🚨 Troubleshooting

### Problemas Comuns

#### Erro de versão do Angular CLI

Garanta que está utilizando a versão correta:

```bash
ng version
```

Caso necessário, atualize:

```bash
npm update -g @angular/cli
```

#### Falha na instalação de pacotes

Limpe o cache e reinstale as dependências:

```bash
npm cache clean --force
rm -rf node_modules package-lock.json
npm install
```

#### Erros de CORS

Caso ocorra erro de CORS, configure o proxy ou ajuste as configurações de CORS no backend:

```bash
ng serve --proxy-config proxy.conf.json
```

