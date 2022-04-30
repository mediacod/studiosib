import React, {useState} from 'react';
import {Button, Form, Input} from "antd-mobile";
import {createAccount} from "../../../../api/api";
import styled from "styled-components";

export const SignUpFormMobile = () => {

    const [error, setError] = useState('')
    const [success, setSuccess] = useState(false)

    const onFinish = async (values: any) => {
        try {
            const response = await createAccount({...values, idTypeAccount: 4})
            setSuccess(true)
        }catch (e) {
            setError('Ошибка регистрации. Попробуйте другой логин')
            setSuccess(false)
        }
    }

    return (
         <>
            <StyledTitle>Создать новый аккаунт</StyledTitle>
            {error && <StyledError>{error}</StyledError>}
             {!success &&<Form
                    onFinish={onFinish}
                    footer={
                        <Button block type='submit' color='primary' size='middle'>
                            Зарегистрироваться
                        </Button>
                    }
                >
                    <Form.Item name='login' label='логин'rules={[{ required: true, message: 'логин не введен' }]} >
                        <Input placeholder='введите логин' />
                    </Form.Item>
                    <Form.Item name='password' label='пароль' rules={[{ required: true, message: 'пароль не введен' }]}>
                        <Input type='password' placeholder='введите пароль'  />
                    </Form.Item>
                </Form>}
             {success && <StyledSuccess>{'Вы успешно зарегистрировались'}</StyledSuccess>}
        </>
    );
};


const StyledTitle = styled.div`
    font-size: 22px;
    font-weight: 700;
    line-height: 2.5;
    margin-left: 10px;
`

const StyledSuccess = styled.div`
  font-size: 14px;
  line-height: 2.5;
  margin-left: 10px;
  color: #50c55c;
`

const StyledError = styled.div`
  font-size: 14px;
  line-height: 2.5;
  margin-left: 10px;
  color: #ff601c;
`