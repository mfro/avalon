import Template from './layout.html';

import * as socket from 'socket';
import * as images from '../images';

export default Template({
    data() {
        return {
            cards: null,
        };
    },

    created() {
        socket.listen('cards', list => this.cards = list);
    },

    methods: {
        toggle(card) {
            socket.send('toggle-card', card.key)
        },

        getImage(card) {
            return images.get(card.key);
        }
    }
});