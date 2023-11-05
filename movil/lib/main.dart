import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'actividades.dart';
import 'estado.dart';
import 'package:geolocator/geolocator.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
      create: (context) => ActivityProvider(),
      child: MaterialApp(
        title: 'Registro de Actividades',
        home: ActivityScreen(),
      ),
    );
  }
}

class ActivityScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final activityProvider = Provider.of<ActivityProvider>(context);
    return Scaffold(
      appBar: AppBar(
        title: Text('Registro de Actividades'),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            ElevatedButton(
              // Cambiado de RaisedButton a ElevatedButton
              onPressed: () async {
                Position position = await Geolocator.getCurrentPosition(
                  desiredAccuracy: LocationAccuracy.high,
                );
                Actividad activity = Actividad(
                  startTime: DateTime.now(),
                  kilometers: position.latitude + position.longitude,
                );
                activityProvider.addActivity(activity);
              },
              child: Text('Iniciar Actividad'),
            ),
            Consumer<ActivityProvider>(
              builder: (context, provider, child) {
                return Column(
                  children: provider.actividades.map((activity) {
                    Duration duration =
                        DateTime.now().difference(activity.startTime);
                    return ListTile(
                      title: Text(
                          'Tiempo: ${duration.inMinutes} minutos ${duration.inSeconds % 60} segundos'),
                      subtitle: Text('Kilómetros: ${activity.kilometers}'),
                    );
                  }).toList(),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
