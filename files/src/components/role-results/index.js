import Template from './layout.html';

import * as images from '../images';

export default Template({
    props: {
       info: Object
    },

    computed: {
        image() {
            return images.get(this.info.card.key);
        }
    },

    methods: {
        makeList(list) {
            if (list.length == 1)
                return list[0];
            if (list.length == 2)
                return `${list[0]} and ${list[1]}`;

            let str = ''
            for (let i = 0; i < list.length - 1; i++) {
                str += list[i] + ', ';
            }
            return str + 'and ' + list[list.length - 1];
        },
    }
});
