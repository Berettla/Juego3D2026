# Evaluación Sumativa N°03

Proyecto 3D en **Unity 6000.2.3f1** (plantilla Universal 3D / URP). Una esfera recorre un circuito con desnivel, rampa, saltos y obstáculos, recolectando ítems.

> **Estado:** módulos 1 al 3 terminados (escenario, modelo compuesto, movimiento y salto). **Faltan los módulos 4, 5 y 6** (ver sección "Tu parte").

---

## 1. Cómo abrir el proyecto

1. Instala **exactamente Unity 6000.2.3f1** desde Unity Hub.
2. Clona el repositorio: `git clone https://github.com/Berettla/Juego3D2026.git`
3. En Unity Hub: **Add > Add project from disk**, elige la carpeta clonada y ábrela con 6000.2.3f1.
4. La primera apertura tarda varios minutos (genera `Library`, que no se sube a Git). Es normal.
5. Abre la escena `Assets/Scenes/SampleScene` y da Play para comprobar que todo funciona.

**Reglas de trabajo con Git (importante):**
- Antes de empezar: `git pull`. Al terminar cada bloque: `git add .`, `git commit -m "..."`, `git push`.
- **No subas ni borres archivos `.meta`.** Unity los usa para mantener las referencias.
- Solo **una persona a la vez** debe modificar la escena. Mientras una trabaja, la otra no toca `SampleScene`.
- Si Unity pregunta si quieres recargar la escena tras un `pull`, acepta.

---

## 2. Lo que ya está hecho

### Escenario (criterio A, completo)
Todo está dentro del contenedor **Escenario** (más **Paredes**, **Modelos** y **UI**):

| Zona | Descripción |
|---|---|
| Suelo (plataforma baja) | 20x20, con paredes. Punto de inicio del jugador en (0, 0.5, 0) |
| Rampa | Sube hacia +Z hasta la plataforma alta |
| PlataformaAlta | Superficie a Y = 2, con borde abierto al este (por ahí se cae) |
| Piedra1 a 3, Puente, PlataformaFinal | Ruta de saltos hacia el este (+X) |
| PistaHielo | Material físico sin fricción, el jugador resbala |
| Descanso | Plataforma de reposo antes de la barra |
| ArenaGiratoria + BarraGiratoria | Barra que gira (script `BarraGiratoria`) y empuja al jugador |
| CaedizaA, B y C | Plataformas que se desactivan al pisarlas y **reaparecen solas** a los 3 s |
| PlataformaMeta | Final del recorrido (aprox. Z = 83) |

- Modelo 3D compuesto: prefab **Arbol** (tronco y copas), colocado en varias zonas.
- Materiales personalizados: Fondo, Oro, Plataforma, Especial, Jugador, Madera, Hojas, Hielo (más el Physics Material Hielo).
- Directional Light ajustada.

### Movimiento (criterio B, completo)
- Script `movimiento.cs` en el **Jugador**: movimiento con `AddForce`, `velocidad` pública, **salto controlado** (Raycast hacia abajo, sin saltos infinitos en el aire).
- La cámara (`CamaraController`) sigue al jugador con un offset fijo.

### Lo que existe de coleccionables (parcial)
- Un prefab **Coleccionable** (cubo dorado, material Oro, Is Trigger, Rigidbody cinemático, script `Rotador`, tag `Coleccionable`).
- `movimiento.cs` ya detecta ese tag en `OnTriggerEnter`, lo desactiva y suma 1, pero con un solo tipo y sin meta.

---

## 3. Tu parte: lo que falta

### Módulo 4: Caída al vacío y respawn (criterio C)
- Detectar cuando el jugador cae fuera del escenario y reposicionarlo en el punto inicial (0, 0.5, 0), **sin conservar la velocidad previa** (esto distingue "Sobresaliente" de "Logrado").
- Extra opcional: punto de control. El recorrido es largo y volver siempre al inicio castiga mucho.

### Módulo 5: Segundo tipo de coleccionable (criterio D)
- Crear el prefab del coleccionable especial (+3) con tag, material y velocidad de giro distintos al normal. Hoy `Rotador` tiene la velocidad fija en el código, así que habrá que hacerla configurable.
- Distribuir en el mapa mínimo **6 normales y 3 especiales**, usando todo el recorrido. ahi pones la ubicación donde quieras.

### Módulo 6: Puntaje diferenciado y reinicio (criterio E)
- Distinguir ambos tipos de ítem en `movimiento.cs`, sumando su valor y mostrando por consola el detalle de cada objeto recogido y el puntaje acumulado.
- Mantener actualizado el Texto Contador de la UI.
- Al llegar a **12 puntos o más**, reiniciar la partida completa (recargar la escena, no solo la variable del puntaje).

---

## 4. Nombres que NO deben cambiarse

| Elemento | Nombre exacto |
|---|---|
| Script del jugador | `movimiento` (archivo `movimiento.cs`) |
| Script de la cámara | `CamaraController` |
| Script de rotación | `Rotador` |
| Tag normal | `Coleccionable` |
| Tag especial (a crear) | `Especial` |
| Campo UI del jugador | `puntaje` (arrastrado desde **Texto Contador**) |
| Plataformas caedizas | usan `PlataformaCaediza`, que identifica al jugador por el script `movimiento` |

Si renombras algo, revisa que no se rompa ninguna referencia en el Inspector.

---

## 5. Cumplimiento de la rúbrica (resumen)

| Criterio | Estado |
|---|---|
| A. Escenario con desnivel y modelos 3D | Hecho |
| B. Movimiento físico + salto | Hecho |
| C. Cámara y caída al vacío | Cámara hecha, **falta el respawn** |
| D. Dos coleccionables y Prefabs | **Falta el especial** y distribuir ítems |
| E. Recolección diferenciada y reinicio | **Falta** (+3, consola, meta de 12 y recarga) |

