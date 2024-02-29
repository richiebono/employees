import polyglotI18nProvider from 'ra-i18n-polyglot';
import englishMessages from '../../i18n/en';

const messages = {
    fr: () => import('../../i18n/fr').then(messages => messages.default),
    pt: () => import('../../i18n/pt-BR').then(messages => messages.default),
    es: () => import('../../i18n/es').then(messages => messages.default),
};

export default polyglotI18nProvider(
    locale => {
        if (locale === 'en') {
            return englishMessages;
        }               
        return messages[locale]();
    },
    'en',
    [
        { locale: 'en', name: 'English' },
        { locale: 'fr', name: 'Français' },
        { locale: 'pt', name: 'Português' },
        { locale: 'es', name: 'Español' },
    ]
);
