# Juego3D2026

Evaluación Sumativa N°03: Construcción Base del Videojuego 3D
Desarrollo de Video Juegos (TILE22), INACAP

**Integrantes:** Agustín Inzunza y Yordy Montecino
**Motor:** Unity 6000.2.3f1 (Universal 3D / URP)
**Escena principal:** `Assets/Scenes/SampleScene`

## Descripción

Una esfera recorre un circuito de plataformas recogiendo ítems. El objetivo es juntar **12 puntos** antes de que se acabe el tiempo. Si se llega a la meta, la partida se reinicia; si el tiempo llega a 0, la esfera explota y también se reinicia.

## Controles

- **Flechas o WASD:** mover la esfera
- **Espacio:** saltar (solo apoyado en una superficie)

## Qué incluye

- **Escenario:** plataforma baja, rampa, plataforma alta, ruta de saltos, puente y tramo final. Hay bordes abiertos, así que se puede caer.
- **Obstáculos:** pista de hielo, barra giratoria y plataformas que caen al pisarlas y reaparecen.
- **Modelo 3D compuesto:** árbol hecho con varias piezas, guardado como prefab.
- **Coleccionables:** normal (+1) y especial (+3), cada uno con su prefab, tag, material y giro distintos.
- **Caída al vacío:** al caer fuera del escenario, el jugador vuelve al inicio sin conservar velocidad.
- **Puntaje y tiempo:** se muestran en pantalla y el detalle de cada ítem recogido se imprime en consola.

## Cómo abrirlo

1. Abrir el proyecto con Unity 6000.2.3f1 desde Unity Hub.
2. Abrir la escena `SampleScene` y presionar Play.
