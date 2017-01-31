import Template from './layout.html';

import * as socket from 'socket';
import LoginPage from '../login-page';
import RoleResults from '../role-results';
import CardSelector from '../card-selector';

export default Template({
    components: {
        LoginPage,
        RoleResults,
        CardSelector
    },

    data() {
        return {
            info: null,

            selfId: null,
            members: null,
        };
    },

    computed: {
        self() {
            return this.members && this.members.find(s => s.id == this.selfId);
        },
    },

    created() {
        socket.on('close', () => {
            this.info || this.$reset();
        });

        socket.listen('info', info => {
            this.info = info;
            socket.close();
        });

        socket.listen('self', self => this.selfId = self.id);
        socket.listen('members', list => this.members = list);
    },

    methods: {
        ready() {
            socket.send('ready');
        },
    }
});
