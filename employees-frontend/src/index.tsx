/* eslint react/jsx-key: off */
import * as React from 'react';
import { Admin, Resource } from 'react-admin'; // eslint-disable-line import/no-unresolved
import { render } from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import authProvider from './utils/providers/authProvider';
import dataProvider from './utils/providers/dataProvider';
import i18nProvider from './utils/providers/i18nProvider';
import Layout from './Layout';
import employees from './employees';
import './style.css';

render(
    <React.StrictMode>
        <BrowserRouter>
                <Admin
                    authProvider={authProvider}
                    dataProvider={dataProvider}
                    i18nProvider={i18nProvider}
                    title="Example Admin"
                    layout={Layout as any}
                >            
                    <Resource name="Employee" {...employees} />            
                </Admin>
        </BrowserRouter>
    </React.StrictMode>,
    document.getElementById('root')
);
