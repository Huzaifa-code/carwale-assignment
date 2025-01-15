import { createStore } from 'redux';
import filterReducer from './reducers/filterReducer';

const store = createStore(
    filterReducer,
    window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__() // to enable Redux DevTool for debugging
);

export default store;
