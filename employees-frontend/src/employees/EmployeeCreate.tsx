import * as React from 'react';
import { useMemo } from 'react';
import {
    Create,
    required,
    ReferenceInput,
    SaveButton,
    SelectInput,
    SimpleFormConfigurable,
    TextInput,
    Toolbar,
    useNotify,
    usePermissions,
    useRedirect,
    useCreate,
    useCreateSuggestionContext,
} from 'react-admin';
import { useFormContext, useWatch } from 'react-hook-form';
import { Button, Dialog, DialogActions, DialogContent } from '@mui/material';

const EmployeeCreateToolbar = () => {
    const notify = useNotify();
    const redirect = useRedirect();
    const { reset } = useFormContext();

    return (
        <Toolbar>
            <SaveButton label="employee.action.save_and_edit" variant="text" />
            <SaveButton
                label="employee.action.save_and_show"
                type="button"
                variant="text"
                mutationOptions={{
                    onSuccess: data => {
                        notify('ra.notification.created', {
                            type: 'info',
                            messageArgs: { smart_count: 1 },
                        });
                        redirect('show', 'employees', data.id);
                    },
                }}
            />
            <SaveButton
                label="employee.action.save_and_add"
                type="button"
                variant="text"
                mutationOptions={{
                    onSuccess: () => {
                        reset();
                        window.scrollTo(0, 0);
                        notify('ra.notification.created', {
                            type: 'info',
                            messageArgs: { smart_count: 1 },
                        });
                    },
                }}
            />            
        </Toolbar>
    );
};

const EmployeeCreate = () => {
          
   
    const { permissions } = usePermissions();
    const dateDefaultValue = useMemo(() => new Date(), []);
    return (
        <Create redirect="edit">
            <SimpleFormConfigurable
                toolbar={<EmployeeCreateToolbar />}
            >
                <ReferenceInput label="Employee Type" source="employeeTypeId" reference="EmployeeType">
                    <SelectInput source='id' optionText="type" validate={required("Required field")}  />
                </ReferenceInput>
                
                <TextInput
                    source="firstName"
                    fullWidth
                    multiline
                    validate={required('Required field')}
                />

                <TextInput
                    source="lastName"
                    fullWidth
                    multiline
                    validate={required('Required field')}
                />    

                <TextInput
                    source="email"
                    fullWidth
                    multiline
                    validate={required('Required field')}
                />

                <TextInput
                    source="jobTitle"
                    fullWidth
                    multiline
                    validate={required('Required field')}
                />    

                <TextInput
                    source="dateOfJoining"
                    fullWidth
                    multiline
                    defaultValue={dateDefaultValue}
                    validate={required('Required field')}
                />

            </SimpleFormConfigurable>
        </Create>
    );
};

export default EmployeeCreate;

const DependantInput = ({
    dependency,
    children,
}: {
    dependency: string;
    children: JSX.Element;
}) => {
    const dependencyValue = useWatch({ name: dependency });

    return dependencyValue ? children : null;
};

const CreateUser = () => {
    const { filter, onCancel, onCreate } = useCreateSuggestionContext();
    const [value, setValue] = React.useState(filter || '');
    const [create] = useCreate();

    const handleSubmit = ({event} : any) => {
        event.preventDefault();
        create(
            'users',
            {
                data: {
                    name: value,
                },
            },
            {
                onSuccess: data => {
                    setValue('');
                    onCreate(data);
                },
            }
        );
    };

    return (
        <Dialog open onClose={onCancel}>
            <form onSubmit={handleSubmit}>
                <DialogContent>
                    <TextInput
                        source="firstName"
                        defaultValue=""
                        autoFocus
                        validate={[required()]}
                    />
                    <TextInput
                        source="lastName"
                        defaultValue=""
                        validate={[required()]}
                    />
                    
                    <TextInput
                        source="email"
                        defaultValue=""
                        validate={[required()]}
                    />

                    <TextInput
                        source="jobTitle"
                        defaultValue=""
                        validate={[required()]}
                    />

                    <TextInput
                        source="dateOfJoining"
                        defaultValue=""
                        validate={[required()]}
                    />


                    
                </DialogContent>
                <DialogActions>
                    <Button type="submit">Save</Button>
                    <Button onClick={onCancel}>Cancel</Button>
                </DialogActions>
            </form>
        </Dialog>
    );
};
