import React, {useContext, useState} from 'react';
import {Button, Form, Input} from "antd-mobile";
import AuthContext from "../../../../context/authContext";
import styled from "styled-components";

;

interface IProps {

}

export const LoginFormMobile: React.FC<IProps> = () => {

    const {auth} = useContext(AuthContext)
    const [error, setError] = useState('')

    const onFinish = async (values: any) => {
        const response: any = await auth(values)

        if(!response) {
            setError('ошибка авторизации')
        }
    }

    return (
        <>
            <StyledTitle>Авторизация</StyledTitle>
            {error && <StyledError>{error}</StyledError>}
            <Form
                onFinish={onFinish}
                footer={
                    <Button block type='submit' color='primary' size='middle'>
                        Войти
                    </Button>
                }
            >
                <Form.Item name='login' label='логин'rules={[{ required: true, message: 'логин не введен' }]} >
                    <Input placeholder='введите логин' />
                </Form.Item>
                <Form.Item name='password' label='пароль' rules={[{ required: true, message: 'пароль не введен' }]}>
                    <Input type='password' placeholder='введите пароль'  />
                </Form.Item>
            </Form>
        </>
    );
};

const StyledTitle = styled.div`
    font-size: 22px;
    font-weight: 700;
    line-height: 2.5;
    margin-left: 10px;
`

const StyledError = styled.div`
  font-size: 14px;
  line-height: 2.5;
  margin-left: 10px;
  color: #ff601c;
`