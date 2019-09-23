//https://stackoverflow.com/questions/14297625/work-with-a-time-span-in-javascript

function calcularProgresso(inicio, fim) {

    var hoje = Date.now();

    inicio = new Date(inicio);
    fim = new Date(fim);

    var agoracomeco = hoje.getTime() - inicio.getTime();

    var difcomeco = Math.floor(agoracomeco / (1000 * 60 * 60 * 24));
    agoracomeco -= difcomeco * (1000 * 60 * 60 * 24);


    var fimcomeco = fim.getTime() - inicio.getTime();

    var diffim = Math.floor(fimcomeco / (1000 * 60 * 60 * 24));
    fimcomeco -= diffim * (1000 * 60 * 60 * 24);

    var progresso = ((100 * agoracomeco) / fimcomeco);

    return progresso
};

/*
 var hoje = Date.now();

    var inicio = new Date("7/Nov/2012 20:30:00");
    var fim = new Date("20/Nov/2012 19:15:00");

    var agoracomeco = (new Date(hoje)).getTime() - (new Date(inicio)).getTime();

    var difcomeco = Math.floor(agoracomeco / (1000 * 60 * 60 * 24));
    agoracomeco -= difcomeco * (1000 * 60 * 60 * 24);


    var fimcomeco = (new Date(fim)).getTime() - (new Date(inicio)).getTime();

    var diffim = Math.floor(fimcomeco / (1000 * 60 * 60 * 24));
    fimcomeco -= diffim * (1000 * 60 * 60 * 24);

    var progresso = ((100 * agoracomeco) / fimcomeco);

    document.write(progresso);
 */