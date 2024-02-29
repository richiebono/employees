import {
    TopToolbar,    
    Edit,
    CloneButton,
    CreateButton,
    ShowButton,
    FormTab,
   
    ReferenceInput,
    SelectInput,
    TabbedForm,
    TextInput,
    required,
    EditActionsProps,
} from 'react-admin'; // eslint-disable-line import/no-unresolved
import {
    Box,
    BoxProps,
} from '@mui/material';
import EmployeeTitle from './EmployeeTitle';

const EditActions = ({ hasShow }: EditActionsProps) => (
    <TopToolbar>
        <CloneButton className="button-clone" />
        {hasShow && <ShowButton />}
        {/* FIXME: added because react-router HashHistory cannot block navigation induced by address bar changes */}
        <CreateButton />
    </TopToolbar>
);

const SanitizedBox = ({
    fullWidth,
    ...props
}: BoxProps & { fullWidth?: boolean }) => <Box {...props} />;

const EmployeeEdit = () => {
    return (
        <Edit title={<EmployeeTitle />} actions={<EditActions />}>
            <TabbedForm
                defaultValues={{ average_note: 0 }}
                warnWhenUnsavedChanges
            >
                <FormTab label="employee.form.summary">
                    <SanitizedBox
                        display="flex"
                        flexDirection="column"
                        width="100%"
                        justifyContent="space-between"
                        fullWidth
                    >
                        <TextInput disabled source="id" />
                        
                    </SanitizedBox>
                    <ReferenceInput label="Employee Type" source="employeeTypeId" reference="EmployeeType">
                        <SelectInput source='id' optionText="type" validate={required("Required field")}  />
                    </ReferenceInput>
                    
                    <TextInput
                        source="firstName"
                        fullWidth                        
                        validate={required('Required field')}
                    />     

                    <TextInput
                        source="lastName"
                        fullWidth
                        validate={required('Required field')}
                    />

                    <TextInput
                        source="email"
                        fullWidth
                        validate={required('Required field')}
                    />

                    <TextInput
                        source="jobTitle"
                        fullWidth
                        validate={required('Required field')}
                    />
                    
                </FormTab>
            </TabbedForm>
        </Edit>
    );
};

export default EmployeeEdit;
