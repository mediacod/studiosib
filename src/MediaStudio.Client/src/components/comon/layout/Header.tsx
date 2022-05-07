import React from 'react';
import styled from "styled-components";
import {PrevPage} from "../../icons/PrevPage";
import Tg from "../../icons/Tg";
import Yt from "../../icons/Yt";
import FormSearch from "../../formSearch";
import {ButtonIcon} from "../../../mobile/components/blocks/ButtonIcon";

function NextPage(props: { color: string, size: string, className: string, height: string }) {
    return null;
}

export const Header: React.FC = () => {

    const targetTg = () => {
        // router.push('https://t.me/studiosib_music', '_blank')
    }

    const targetYt = () => {
        // router.push('https://www.youtube.com/channel/UCx9cXwpj_scHczd4AIfsfPg/featured', '_blank')
    }



    return (
        <StyledHeader>
            <StyledNavigation>
                <StyledNavButton className={'Prev'} onClick={()=>{}}>
                    <StyledPrevIcon className={'IconPrev'} color={'#3A2C51'} size={'11px'} height={'19px'}/>
                </StyledNavButton>

                <StyledNavButton className={'Next'} onClick={() =>{}}>
                    <StyledNextIcon className={'IconNext'} color={'#3A2C51'} size={'11px'} height={'19px'}/>
                </StyledNavButton>

            </StyledNavigation>

            <StyledSearch onClick={()=>{}}>
                <FormSearch />
            </StyledSearch>

            <StyledAuth>
                <StyledContact>
                    <ButtonIcon size={30}>
                        <Tg action={targetTg} color={''} size={30} className={'contactIcon'}/>
                    </ButtonIcon>

                    <ButtonIcon size={30}>
                        <Yt action={targetYt} color={''} size={30} className={'contactIcon'}/>
                    </ButtonIcon>

                    </StyledContact>

                <StyledName>Имя пользователя</StyledName>
            </StyledAuth>
        </StyledHeader>
    );
};

const StyledHeader = styled.div`
  grid-area: hd;
  height: 100%;
  display: flex;
  flex-wrap: nowrap;
  align-items: flex-start;
  margin: 41px 45px 0 41px;
  //margin: 10px 45px 0 41px;
  background-color: #fff;

  &.transparent {
    background-color: #fff0;
  }
`

const StyledNavigation = styled.div`
  width: 80px;
  display: flex;
  flex-wrap: nowrap;
  justify-content: space-between;
  margin-right: 5.916em;
`

const StyledNavButton = styled.div`
  position: relative;
  display: block;
  background-color: #fff;
  background-repeat: no-repeat;
  width: 34px;
  height: 34px;
  border-radius: 50%;
  box-shadow: 0px 4px 7px rgba(0, 0, 0, 0.12);
`

const StyledPrevIcon = styled(PrevPage)`
  position: absolute;
  left: 9px;
  top: 8px;
`

const StyledNextIcon = styled(NextPage)`
  position: absolute;
  left: 14px;
  top: 8px;
`

const StyledSearch = styled.div`
  width: 295px;
  border-bottom: 1px solid #000;
  padding-bottom: 0;

  form {
    display: flex;
    flex-wrap: nowrap;
    justify-content: space-between;

    input[type="search"] {
      background-color: transparent;
      border: none;
      outline: none;
      width: 93%;
      font-size: 16px;
      line-height: 1em;
      color: #5F5A5A;

      &::-ms-clear {
        display: none;
      }

      &::-webkit-search-cancel-button {
        display: none;
      }
    }

    button[type="button"] {
      align-self: flex-end;
      background-image: url("./../public/icon/search.svg");
      background-color: transparent;
      background-repeat: no-repeat;
      background-size: contain;
      background-position-y: 70%;
      width: 18px;
      height: 26px;
      border: none;
      outline: none;
      cursor: pointer;
    }
  }
`

const StyledAuth = styled.div`
  flex-grow: 1;
  display: flex;
  justify-content: flex-end;
  align-items: flex-start;
`

const StyledName = styled.span`
  font-size: 16px;
  font-weight: 400;
  line-height: 22px;
  vertical-align: middle;
  color: #5F5A5A;
  margin-left: 34px;
`

const StyledContact = styled.div`
  display: flex;
  align-items: center;

  &Icon {
    display: flex;
    flex-direction: row;
    gap: 1rem;
    align-items: center;
    margin-left: 14px;
  }
`