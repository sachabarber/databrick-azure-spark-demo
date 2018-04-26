import org.apache.spark.sql.SparkSession

object SparkApp extends App {

  Console.println("===== Starting =====")

  val multiplier = 2
  Console.println(s"multiplier is set to $multiplier")

  val session = SparkSession.builder().getOrCreate()
  val sparkContext = session.sparkContext
  val sqlContext = session.sqlContext

  import sqlContext.implicits._

  val data = Array(1, 2, 3, 4, 5)
  val dataRdd = sparkContext.parallelize(data)
  val mappedRdd = dataRdd.map(_ * multiplier)
  val df = mappedRdd.toDF

  df.show()

  Console.println("===== Done =====")
}