import BookIcon from '@mui/icons-material/Book';
import EmployeeCreate from './EmployeeCreate';
import EmployeeEdit from './EmployeeEdit';
import EmployeeList from './EmployeeList';
import EmployeeShow from './EmployeeShow';

export default {
    list: EmployeeList,
    create: EmployeeCreate,
    edit: EmployeeEdit,
    show: EmployeeShow,
    icon: BookIcon,
    recordRepresentation: 'title',
};
