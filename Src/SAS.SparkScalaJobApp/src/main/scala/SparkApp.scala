import scala.util.Try
import scala.util.Success
import scala.util.Failure
import org.apache.spark.sql.SparkSession

object SparkApp extends App {

  println("===== Starting =====")

  if(args.length != 1) {
    println("Need exactly 1 int arg")
  }

  Try {
    Integer.parseInt(args(0))
  } match {
    case Success(v:Int) => {
      val combinedArgs = args.aggregate("")(_ + _, _ + _)
      println(s"Args were $combinedArgs")
      SparkDemoRunner.runUsingArg(v)
      println("===== Done =====")
    }
    case Failure(e) =>  {
      println(s"Could not parse command line arg [$args(0)] to Int")
      println("===== Done =====")
    }
  }
}

object SparkDemoRunner {
  def runUsingArg(howManyItems : Int) : Unit =  {
    val session = SparkSession.builder().getOrCreate()
    import session.sqlContext.implicits._

    val multiplier = 2
    println(s"multiplier is set to $multiplier")

    val multiplierBroadcast = session.sparkContext.broadcast(multiplier)
    val data = List.tabulate(howManyItems)(_ + 1)
    val dataRdd = session.sparkContext.parallelize(data)
    val mappedRdd = dataRdd.map(_ * multiplierBroadcast.value)
    val df = mappedRdd.toDF
    df.show()
  }
}



