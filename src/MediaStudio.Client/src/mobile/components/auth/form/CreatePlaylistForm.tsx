import React, {FC} from 'react';
import {Button, Form, Input, Space, Radio} from "antd-mobile";
import {useUserPlaylistQuery} from "../../../../query/useUserPlaylistQuery";
import {useColorQuery} from "../../../../query/useColorQuery";
import styled from "styled-components";

interface IProps {
    toggleVisible: (isOpen?: boolean)=>void;
}

export const CreatePlaylistForm:React.FC<IProps> = ({toggleVisible}) => {

    const {createPlaylistMutation} = useUserPlaylistQuery()
    const {data: colors} = useColorQuery()
    const [form] = Form.useForm()

    const onFinish = async (values: any) => {
        console.log(values)
        createPlaylistMutation.mutate(values)
        form.resetFields()
        toggleVisible(false)
    }

    return (
        <Form
            mode='card'
            form={form}
            onFinish={onFinish}
            footer={
                <Button
                    block
                    type='submit'
                    color='primary'
                    size='middle'
                >
                    создать плейлист
                </Button>
            }>
            <Form.Header>Создать плейлист</Form.Header>
            <Form.Item name='name' label='Название' rules={[{ required: true }]}>
                <Input placeholder='Введите название' />
            </Form.Item>
            <Form.Item name='idColour' label='Цвет' rules={[{ required: true }]}>
                <Radio.Group>
                    <Space wrap>
                        {colors?.map((c: any) =>
                            <Radio value={c.idColour} key={c.idColour}>
                                <StyledRadioColor color={c.code}/>
                            </Radio>)}
                    </Space>
                </Radio.Group>
            </Form.Item>
        </Form>
    );
};

const StyledRadioColor = styled.div<{color: string}>`
  background-color: ${({color})=> color};
  width: 30px;
  height: 30px;
  display: block;
  border-radius: 100%;
`