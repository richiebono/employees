import { useTranslate, useRecordContext } from 'react-admin';

export default () => {
    const translate = useTranslate();
    const record = useRecordContext();
    return (
        <>
            {record
                ? translate('employee.edit.title', { title: record.firstName + ' ' + record.employeetTypeName })
                : ''}
        </>
    );
};
