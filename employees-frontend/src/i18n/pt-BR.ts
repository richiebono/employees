import portugueseMessages from 'ra-language-portuguese';

export const messages = {
    simple: {
        action: {
            close: 'Fechar',
            resetViews: 'Limpar',
        },
        'create-employee': 'Novo Empregado',
    },
    ...portugueseMessages,
    resources: {
        employees: {
            name: 'Empregado |||| Empregado',
            fields: {
                id: 'Código do Empregado',
                firstName: 'Nome do Cliente',
                dateCreated: 'Data de Criação',
                dateUpdated: 'Data de Atualização',
                employeeTypeName: 'Tipo de Empregado',
                userName: 'Usuário',
            },
        }
    },
    employee: {
        list: {
            search: 'Buscar',
        },
        form: {
            summary: 'Resumo'
        },
        edit: {
            title: 'Empregados',
        },
        action: {
            save_and_edit: 'Salvar e Alterar',
            save_and_add: 'Salvar e Adicionar',
            save_and_show: 'Salvar E Visualizar',
        },
    }    
};

export default messages;
