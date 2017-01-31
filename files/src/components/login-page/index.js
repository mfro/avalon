import Template from './layout.html';

import * as socket from 'socket';

export default Template({
    data() {
        return {
            name: '',
            error: null,
            loading: false
        }
    },

    created() {
        let saved = localStorage.getItem('avalon.name');
        if (saved) {
            this.name = saved;
        }
    },

    methods: {
        submit() {
            localStorage.setItem('avalon.name', this.name);
            this.loading = true;
            socket.create(this.name, () => {
                this.loading = false;
                this.error = 'Name already taken';
            });
        }
    }
});