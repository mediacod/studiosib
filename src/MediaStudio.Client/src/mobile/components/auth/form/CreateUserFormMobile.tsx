import React, {useContext, useState} from 'react';
import {Button, DatePicker, Form, Input, Selector} from "antd-mobile";
import {USER_API} from "../../../../api/api";
import dayjs from 'dayjs'
import AuthContext from "../../../../context/authContext";

interface IProps {

}

export const options = [
    {
        label: 'Мужской',
        value: '1',
    },
    {
        label: 'Женский',
        value: '2',
    },
]

export const CreateUserFormMobile: React.FC<IProps> = () => {

    const [pickerVisible, setPickerVisible] = useState(false)
    const {isAuth, user, getUser} = useContext(AuthContext)

    const now = new Date()

    const onFinish = async (values: any) => {
        const data = {...values, gender: values.gender[0]}

        try {
            const response = await USER_API.CREATE(data)
            getUser()
        }catch (e) {
            console.log(e)
        }
    }

    if(!isAuth || user){
        return <></>
    }

    return (
        <>
            <Form
                onFinish={onFinish}
                footer={
                    <Button block type='submit' color='primary' size='middle'>
                        Подтвердить
                    </Button>
                }
            >
                <Form.Item name='firstName' label='Имя' rules={[{ required: true }]}>
                    <Input placeholder='Введите полное имя' />
                </Form.Item>
                <Form.Item name='lastName' label='Фамилия' rules={[{ required: true }]}>
                    <Input placeholder='Введите фамилию' />
                </Form.Item>
                <Form.Item name='gender' label='Пол' rules={[{ required: true }]}>
                    <Selector
                        style={{
                            '--border-radius': '100px',
                            '--border': 'solid transparent 1px',
                            '--checked-border': 'solid var(--adm-color-primary) 1px',
                            '--padding': '8px 24px',
                        }}
                        showCheckMark={false}
                        options={options}
                    />
                </Form.Item>
                <Form.Item
                    name='dateBirthday'
                    label='Дата рождения'
                    trigger='onConfirm'
                    rules={[{ required: true }]}
                    onClick={() => {
                        setPickerVisible(true)
                    }}
                >
                    <DatePicker
                        visible={pickerVisible}
                        onClose={() => {
                            setPickerVisible(false)
                        }}
                        min={dayjs().subtract(100, 'year').toDate()}
                        max={now}
                    >
                        {value =>
                            value ? dayjs(value).format('YYYY-MM-DD') : null
                        }
                    </DatePicker>
                </Form.Item>
                <Form.Item name='phoneNumber'
                           label='Телефон'>
                    <Input placeholder='Номер телефона' />
                </Form.Item>
            </Form>
        </>
    );
};

// {
//     "idUser": 0,
//     "firstName": "string",
//     "lastName": "string",
//     "patronymic": "string",
//     "gender": 0,
//     "dateBirthday": "2022-03-26T11:12:33.595Z",
//     "phoneNumber": "string",
//     "idCloudPath": 0,
//     "idAccount": 0
// }