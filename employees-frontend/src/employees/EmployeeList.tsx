import { Fragment, memo } from 'react';
import BookIcon from '@mui/icons-material/Book';
import { Box, Chip, useMediaQuery } from '@mui/material';
import { Theme, styled } from '@mui/material/styles';
import jsonExport from 'jsonexport/dist';
import {
    BulkDeleteButton,
    BulkExportButton,
    SelectColumnsButton,
    CreateButton,
    DatagridConfigurable,
    DateField,
    downloadCSV,
    EditButton,
    ExportButton,
    FilterButton,
    List,
    SearchInput,
    ShowButton,
    SimpleList,
    TextField,
    TopToolbar,
    useTranslate,
    ReferenceInput,
    SelectInput,
    required,
} from 'react-admin'; // eslint-disable-line import/no-unresolved

export const EmployeeIcon = BookIcon;

const QuickFilter = ({ label, source, defaultValue }: any) => {
    const translate = useTranslate();
    return <Chip sx={{ marginBottom: 1 }} label={translate(label)} />;
};

const employeeFilter = [
    <SearchInput source="firstName" alwaysOn />,
    <ReferenceInput label="resources.employees.fields.employeeTypeName" source="employeeTypeId" reference="EmployeeType" alwaysOn={true}>
        <SelectInput source='id' optionText="type" defaultValue='2D9D8CF3-80E7-4E0A-B6A4-544B5F169DB5' emptyText="Select..." />
    </ReferenceInput>,    
    <QuickFilter
        label="resources.employees.fields.firstName"
        source="firstName"
    />,
];

const exporter = (Employees: any) => {
   
    const employeesExport = Employees.map((employee: any) => {
        const { id, userId, employeeTypeId, ...employeeForExport } = employee; // omit backlinks and author
        return employeeForExport;
    });

    // change the rowDelimiter to change the CSV file delimiter
    return jsonExport(employeesExport, {rowDelimiter: ';'}, (err: any, csv: any) => downloadCSV(csv, 'Employee'));
};

const StyledDatagrid = styled(DatagridConfigurable)(({ theme }) => ({
    '& .title': {
        maxWidth: '20em',
        overflow: 'hidden',
        textOverflow: 'ellipsis',
        whiteSpace: 'nowrap',
    },
    '& .hiddenOnSmallScreens': {
        [theme.breakpoints.down('lg')]: {
            display: 'none',
        },
    },
    '& .column-tags': {
        minWidth: '9em',
    },
    '& .publishedAt': { fontStyle: 'italic' },
}));

const EmployeeListBulkActions = memo(({ children, ...props }: any) => (
    <Fragment>
        <BulkDeleteButton {...props} />
        <BulkExportButton {...props} />
    </Fragment>
));

const EmployeeListActions = () => (
    <TopToolbar>
        <SelectColumnsButton />
        <FilterButton />
        <CreateButton />
        <ExportButton />
    </TopToolbar>
);

const EmployeeListActionToolbar = ({ children, ...props }: any) => (
    <Box sx={{ alignItems: 'center', display: 'flex' }}>{children}</Box>
);

const rowClick = ({id, resource, record}: any) => {
    if (id) {
        return 'edit';
    }

    return 'show';
};

const EmployeeList = () => {
    const isSmall = useMediaQuery<Theme>(theme => theme.breakpoints.down('md'));
    return (
        <List
            filters={employeeFilter}
            sort={{ field: 'dateCreated', order: 'DESC' }}
            exporter={exporter}
            actions={<EmployeeListActions />}
        >
            {isSmall ? (
                <SimpleList
                    primaryText={record => record.id}
                    secondaryText={record => record.publishedAt}
                    tertiaryText={record =>
                        new Date(record.dateCreated).toLocaleDateString()
                    }
                />
            ) : (
                <StyledDatagrid
                    bulkActionButtons={<EmployeeListBulkActions />}
                    rowClick={rowClick}
                >
                    <TextField label="resources.employees.fields.id" source="id" />
                    <TextField label="resources.employees.fields.employeeTypeName" source="employeeTypeName" cellClassName="title" />
                    <TextField label="resources.employees.fields.firstName" source="firstName" cellClassName="title" />
                    <TextField label="resources.employees.fields.lastName" source="lastName" cellClassName="title" />
                    <TextField label="resources.employees.fields.email" source="email" cellClassName="title" />
                    <TextField label="resources.employees.fields.jobTitle" source="jobTitle" cellClassName="title" />
                    <TextField label="resources.employees.fields.dateOfJoining" source="dateOfJoining" cellClassName="title" />
                    <TextField label="resources.employees.fields.userName" source="userName" cellClassName="title" />
                    <DateField
                        label="resources.employees.fields.dateCreated"
                        source="dateCreated"
                        sortByOrder="DESC"
                        cellClassName="publishedAt"
                    />
                    <DateField
                        label="resources.employees.fields.dateUpdated"
                        source="dateUpdated"
                        sortByOrder="DESC"
                        cellClassName="publishedAt"
                    />
                    <EmployeeListActionToolbar>
                        <EditButton />
                        <ShowButton />
                    </EmployeeListActionToolbar>
                </StyledDatagrid>
            )}
        </List>
    );
};

const tagSort = { field: 'name.en', order: 'ASC' };

export default EmployeeList;
