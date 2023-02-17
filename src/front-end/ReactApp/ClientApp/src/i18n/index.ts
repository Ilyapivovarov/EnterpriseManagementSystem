import i18n from 'i18next';
import {initReactI18next} from 'react-i18next';
import ruTranslation from './ru/translation.json';
import enTranslation from './en/translation.json';
import {LocalizationKey} from '../helpers/Constants';

export class TranslationKeys {
  static navMenu = {
    employees: 'navMenu.employees',
    tasks: 'navMenu.tasks',
    settings: 'navMenu.settings',
  };
}

const resources = {
  ru: {
    translation: ruTranslation,
  },
  en: {
    translation: enTranslation,
  },
};

const currentLocalization = localStorage.getItem(LocalizationKey);
if (!currentLocalization) {
  localStorage.setItem(LocalizationKey, navigator.language);
}

i18n.use(initReactI18next).init({
  lng: localStorage.getItem(LocalizationKey)!,
  resources,
});

export default i18n;
