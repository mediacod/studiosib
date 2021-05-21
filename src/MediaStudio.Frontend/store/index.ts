// create a makeStore function
import {Context, createWrapper, MakeStore} from "next-redux-wrapper";
import {reducer, RootState} from "./reducers";
import { createStore, applyMiddleware, Store } from 'redux'
import createSagaMiddleware, {Task} from 'redux-saga';
import rootSaga from "./sagas/rootSaga";


export interface SagaStore extends Store {
    sagaTask?: Task;
}

const makeStore = (context: Context) => {
    // 1: Create the middleware
    const sagaMiddleware = createSagaMiddleware();

    // 2: Add an extra parameter for applying middleware:
    const store = createStore(reducer, applyMiddleware(sagaMiddleware));

    // 3: Run your sagas on server
    (store as SagaStore).sagaTask = sagaMiddleware.run(rootSaga);

    // 4: now return the store:
    return store;
};

// export an assembled wrapper
export const wrapper = createWrapper<Store<RootState>>(makeStore, {debug: true});
// export const wrapper = createWrapper<RootState>(makeStore, {debug: true});
