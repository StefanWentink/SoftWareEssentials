namespace SWE.Model.Interfaces
{
    public interface IWith<out TResult>
    {
        /// <summary>
        /// Returns a new instance of <see cref="TResult"/>
        /// </summary>
        /// <returns></returns>
        TResult With();
    }
}