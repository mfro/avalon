import * as socket from './socket';
import Vue from 'vue';
import App from './components/app';

import '!file-loader?name=index.html!./index.html';
import '!file-loader?name=icon.png!./icon.png';
import './reset.css';

Vue.use(vue => {
    vue.prototype.$reset = function () {
        Object.assign(this.$data, this.$options.data());
    }
});

let app = new Vue(App);
window.addEventListener('load', () => {
    app.$mount('#mount');
});