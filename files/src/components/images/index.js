let imageMap = {
    merlin: [require('./merlin.png')],
    percival: [require('./percival.png')],
    servant: [require('./servant1.png'), require('./servant2.png'), require('./servant3.png'), require('./servant4.png'), require('./servant5.png')],

    assassin: [require('./assassin.png')],
    mordred: [require('./mordred.png')],
    morgana: [require('./morgana.png')],
    oberon: [require('./oberon.png')],
    minion: [require('./minion1.png'), require('./minion2.png'), require('./minion3.png')],
};

export function get(id) {
    let list = imageMap[id];
    let i = Math.floor(Math.random() * list.length);
    return list[i];
}