export default  {

    login: ({ username, password }: any) => {
        
        const request = new Request(`${ process.env.URL_API }/Users/authenticate/`, {
            method: 'POST',
            body: JSON.stringify({ email: username, password }),
            headers: new Headers({ 'Content-Type': 'application/json; charset=utf-8' }),
        });
        return fetch(request)
            .then(response => {
                if (response.status < 200 || response.status >= 300) {
                    throw new Error(response.statusText);
                }
                return response.json();
            })
            .then(response => {
                console.log({ response });
                localStorage.setItem('auth', JSON.stringify(response));
            });
    },
    logout: () => {
        localStorage.removeItem('auth');
        return Promise.resolve();
    },
    checkError: ({ status }: any) => {
        return status === 401 || status === 403
            ? Promise.reject()
            : Promise.resolve();
    },
    checkAuth: () => {
        return localStorage.getItem('auth')
            ? Promise.resolve()
            : Promise.reject();
    },
    getPermissions: () => {
        const role = JSON.parse(localStorage.getItem('auth') as any).roles;
        return Promise.resolve(role);
    },
    getIdentity: () => {
       
        const { id, fullName, avatar } = JSON.parse(localStorage.getItem('auth') as any);
        return Promise.resolve({ id, fullName, avatar });
    },
};