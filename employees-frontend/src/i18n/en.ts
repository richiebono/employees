import englishMessages from 'ra-language-english';

export const messages = {
    simple: {
        action: {
            close: 'Close',
            resetViews: 'Reset views',
        },
        'create-employee': 'New employee',
    },
    ...englishMessages,
    resources: {
        employees: {
            name: 'Employee |||| Employee',
            fields: {
                id: 'Employee ID',
                firstName: 'First Name',
                lastName: 'Last Name',
                email: 'Email',
                jobTitle: 'Job Title',
                dateOfJoining: 'Date of Joining',
                dateCreated: 'Create Date',
                dateUpdated: 'Update Date',
                employeeTypeName: 'Employee Type',
                userName: 'User',
            },
        }
    },
    employee: {
        list: {
            search: 'Search',
        },
        form: {
            summary: 'Summary'
        },
        edit: {
            title: 'employees',
        },
        action: {
            save_and_edit: 'Save and Edit',
            save_and_add: 'Save and Add',
            save_and_show: 'Save and Show',
            save_with_average_note: 'Save with Note',
        },
    }    
};

export default messages;
