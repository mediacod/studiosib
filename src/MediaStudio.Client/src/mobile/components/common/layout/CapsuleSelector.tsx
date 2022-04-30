import React from 'react';
import {CapsuleTabs} from "antd-mobile";
import styled from "styled-components";

interface IProps {
    activeKey: string;
    selectHandler: any;
    data: any;
}

export const CapsuleSelector:React.FC<IProps> = ({activeKey, selectHandler, data}) => {

    const tabs = data?.map((page: any) =>
        <CapsuleTabs.Tab key={page.path} title={page.title}  />
    )

    return (
        <StyledCapsuleTabs activeKey={activeKey} onChange={selectHandler}>
            {tabs}
        </StyledCapsuleTabs>
    );
};

const StyledCapsuleTabs = styled(CapsuleTabs)`
  //margin: 10px 0 0;
  .adm-capsule-tabs-tab {
    //font-size: 12px;
    font-weight: 500;
  }
  
  .adm-capsule-tabs-header {
    border: none;
  }

  .adm-capsule-tabs-tab {
    background-color: #eaeaea;
    border: #6e6e6e;
  }

  .adm-capsule-tabs-tab-active {
    background-color: #6e6e6e;
  }
`