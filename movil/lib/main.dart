import 'dart:async';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:movil/ruta/google_map.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: MyHomePage(),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key? key}) : super(key: key);

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  bool isRunning = false;
  int hours = 0, minutes = 0, seconds = 0;
  double kilometers = 0.0, velocidadPromedio = 0.0;
  final double velocidad = 2.0; // Velocidad constante para prueba
  late Stopwatch stopwatch;
  late Timer? timer; // Marcar como opcional (?)

  List<LatLng> route = []; // Lista para almacenar la ruta

  @override
  void initState() {
    super.initState();
    stopwatch = Stopwatch();
  }

  // Método para recibir actualizaciones de la ruta desde el widget del mapa
  void updateRoute(List<LatLng> updatedRoute) {
    setState(() {
      route = updatedRoute;
    });
  }

  void _updateTimer(Timer timer) {
    if (stopwatch.isRunning) {
      setState(() {
        seconds = stopwatch.elapsed.inSeconds % 60;
        minutes = (stopwatch.elapsed.inSeconds ~/ 60) % 60;
        hours = stopwatch.elapsed.inSeconds ~/ 3600;
        kilometers = velocidad * stopwatch.elapsed.inSeconds / 1000.0;

        if (stopwatch.elapsed.inSeconds > 0) {
          velocidadPromedio = kilometers / stopwatch.elapsed.inSeconds;
        }
      });
    }
  }

  void startTimer() {
    if (!isRunning) {
      stopwatch.start();
      timer = Timer.periodic(const Duration(seconds: 1), _updateTimer);
      setState(() {
        isRunning = true;
      });
    }
  }

  void stopTimer() {
    if (isRunning) {
      stopwatch.stop();
      timer?.cancel(); // Verificar si timer no es nulo antes de cancelar
      setState(() {
        isRunning = false;
      });
    }
  }

  void resetTimer() {
    stopwatch.reset();
    setState(() {
      isRunning = false;
      hours = 0;
      minutes = 0;
      seconds = 0;
      kilometers = 0.0;
      velocidadPromedio = 0.0;
      route = []; // Restablecer la ruta
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('StraviaTEC'),
      ),
      body: Column(
        children: [
          Expanded(
            child: MapWidget(onRouteUpdated: updateRoute),
          ),
          // Textos
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              children: <Widget>[
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: <Widget>[
                      Text(
                        'Tiempo: $hours : $minutes : $seconds ',
                        style: const TextStyle(fontSize: 20),
                      ),
                      Text(
                        'Distancia: ${kilometers.toStringAsFixed(2)} km',
                        style: const TextStyle(fontSize: 20),
                      ),
                      Text(
                        'Velocidad: ${velocidadPromedio.toStringAsFixed(2)} m/s',
                        style: const TextStyle(fontSize: 20),
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ),
          // Botones
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                ElevatedButton(
                  onPressed: startTimer,
                  child: const Text('Iniciar'),
                ),
                ElevatedButton(
                  onPressed: stopTimer,
                  child: const Text('Detener'),
                ),
                ElevatedButton(
                  onPressed: resetTimer,
                  child: const Text('Reiniciar'),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
