import {
    ShowContextProvider,
    ShowView,
    Tab,
    TabbedShowLayout,
    TextField,
    useShowController,
} from 'react-admin';
import EmployeeTitle from './EmployeeTitle';

const EmployeeShow = () => {
    const controllerProps = useShowController();
    return (
        <ShowContextProvider value={controllerProps}>
            <ShowView title={<EmployeeTitle />}>
                <TabbedShowLayout>
                    <Tab label="employee.form.summary">
                    <TextField source="id" />
                    <TextField source="employeeTypeName" cellClassName="title" />
                    <TextField source="firstName" cellClassName="title" />
                    <TextField source="lastName" cellClassName="title" />
                    <TextField source="email" cellClassName="title" />
                    <TextField source="jobTitle" cellClassName="title" />
                    <TextField source="dateOfJoining" cellClassName="title" />
                    </Tab>
                </TabbedShowLayout>
            </ShowView>
        </ShowContextProvider>
    );
};

export default EmployeeShow;
