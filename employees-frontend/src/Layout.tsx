import * as React from 'react';
import { memo } from 'react';
import { ReactQueryDevtools } from 'react-query/devtools';
import { AppBar, Layout, InspectorButton, Toolbar, SelectInputProps } from 'react-admin';
import { Typography } from '@mui/material';
import logo from './assert/logo.png';

const MyAppBar = memo((props: SelectInputProps) => (
    <AppBar sx={{ backgroundColor: "white", color: "gray" } as any} {...props as any}>
        <img src={logo} alt="logo" style={{maxWidth: "30px",marginLeft: 20, marginRight: 20 }} />
        <Typography flex="1" variant="h6" id="react-admin-title" />
    </AppBar>
));

export default (props: SelectInputProps) => (
    <>
        <Layout {...props as any} appBar={MyAppBar as any} />
        <ReactQueryDevtools
            initialIsOpen={false}
            toggleButtonProps={{ style: { width: 20, height: 30, margin: 0 } }}
        />
    </>
);
