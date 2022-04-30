const typeTemplate = '${label} не является действительным ${type}';


export const locale: ILocale = {
    common: {
        confirm: 'Подтвердить',
        cancel: 'Отмена',
        loading: 'Загрузка...'
    },
    Calendar: {
        markItems: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вск'],
        renderYearAndMonth: (year, month) => `${year}/${month}`
    },
    Cascader: {
        placeholder: 'Выберите'
    },
    Dialog: {
        ok: 'OK'
    },
    ErrorBlock: {
        default: {
            title: 'Упс, что-то пошло не так',
            description: 'Пожалуйста, подождите минуту и попробуйте еще раз'
        },
        busy: {
            title: 'Упс, не загружается',
            description: 'Попробуйте обновить страницу'
        },
        disconnected: {
            title: 'Сеть занята',
            description: 'Попробуйте обновить страницу'
        },
        empty: {
            title: 'Ничего не найдено...',
            description: 'Повторить поиск?'
        }
    },
    Form: {
        required: 'Обязательно',
        optional: 'Необязательно',
        defaultValidateMessages: {
            default: 'Ошибка проверки поля для ${label}',
            required: 'Пожалуйста введите ${label}',
            enum: '${label} должен быть один из [${enum}]',
            whitespace: '${label} не может быть пустым символом',
            date: {
                format: '${label} формат даты неверен',
                parse: '${label} не может быть преобразован в дату',
                invalid: '${label} является недопустимой датой'
            },
            types: {
                string: typeTemplate,
                method: typeTemplate,
                array: typeTemplate,
                object: typeTemplate,
                number: typeTemplate,
                date: typeTemplate,
                boolean: typeTemplate,
                integer: typeTemplate,
                float: typeTemplate,
                regexp: typeTemplate,
                email: typeTemplate,
                url: typeTemplate,
                hex: typeTemplate
            },
            string: {
                len: '${label} должен содержать ${len} знаков',
                min: '${label} должен быть не меньше ${min} знаков',
                max: '${label} должен быть не меньше ${max} знаков',
                range: '${label} должен содержать ${min}-${max} знаков'
            },
            number: {
                len: '${label} должно быть равно ${len}',
                min: '${label} должно быть больше ${min}',
                max: '${label} должно быть меньше ${max}',
                range: '${label} должно быть в диапазоне ${min}-${max}'
            },
            array: {
                len: 'Должен быть ${len} ${label}',
                min: 'По крайней мере ${min} ${label}',
                max: 'Самое большее ${max} ${label}',
                range: 'Количество ${label} должно быть между ${min}-${max}'
            },
            pattern: {
                mismatch: '${label} не соответствует шаблону ${pattern}'
            }
        }
    },
    ImageUploader: {
        uploading: 'Загружается...'
    },
    Mask: {
        name: 'Шаблон'
    },
    Modal: {
        ok: 'OK'
    },
    PullToRefresh: {
        pulling: 'Прокрутите вниз, чтобы обновить',
        canRelease: 'Отпустите, чтобы немедленно обновить',
        complete: 'Обновление прошло успешно'
    }
};

interface ILocale {
    common: {
        confirm: string;
        cancel: string;
        loading: string;
    };
    Calendar: {
        markItems: string[];
        renderYearAndMonth: (year: number, month: number) => string;
    };
    Cascader: {
        placeholder: string;
    };
    Dialog: {
        ok: string;
    };
    ErrorBlock: {
        default: {
            title: string;
            description: string;
        };
        busy: {
            title: string;
            description: string;
        };
        disconnected: {
            title: string;
            description: string;
        };
        empty: {
            title: string;
            description: string;
        };
    };
    Form: {
        required: string;
        optional: string;
        defaultValidateMessages: {
            default: string;
            required: string;
            enum: string;
            whitespace: string;
            date: {
                format: string;
                parse: string;
                invalid: string;
            };
            types: {
                string: string;
                method: string;
                array: string;
                object: string;
                number: string;
                date: string;
                boolean: string;
                integer: string;
                float: string;
                regexp: string;
                email: string;
                url: string;
                hex: string;
            };
            string: {
                len: string;
                min: string;
                max: string;
                range: string;
            };
            number: {
                len: string;
                min: string;
                max: string;
                range: string;
            };
            array: {
                len: string;
                min: string;
                max: string;
                range: string;
            };
            pattern: {
                mismatch: string;
            };
        };
    };
    ImageUploader: {
        uploading: string;
    };
    Mask: {
        name: string;
    };
    Modal: {
        ok: string;
    };
    PullToRefresh: {
        pulling: string;
        canRelease: string;
        complete: string;
    };
};
