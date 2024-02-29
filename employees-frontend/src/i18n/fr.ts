import frenchMessages from 'ra-language-french';

export default {
    simple: {
        action: {
            close: 'Fermer',
            resetViews: 'Réinitialiser les vues',
        },
        'create-employee': 'Nouvelle Employé',
    },
    ...frenchMessages,
    resources: {
        employees: {
            name: 'Employé |||| Employé',
            fields: {
                id: 'Numéro de Employé',
                firstName: 'Customer Name',
                lastName: 'Nom de famille',
                email: 'Email',
                jobTitle: 'Titre de l\'emploi',
                dateOfJoining: 'Date de début',
                employeeTypeName: 'Taper de Employé',
                dateCreated: 'Date de création',
                dateUpdated: 'Date de mise à jour',
                userName: 'Utilisatrice',
            },
        },        
        users: {
            name: 'Utilisatrice |||| Utilisatrice',
            fields: {
                name: 'Nom',
                role: 'Rôle',
            },
        },
    },
    employee: {
        list: {
            search: 'Rechercher',
        },
        form: {
            summary: 'Résumé'
        },
        edit: {
            title: 'Employé',
        },
        action: {
            save_and_edit: 'Enregistrer et modifier',
            save_and_add: 'Enregistrer et ajouter',
            save_and_show: 'Enregistrer et afficher',
            save_with_average_note: 'Enregistrer avec note',
        },
    }
    
};
