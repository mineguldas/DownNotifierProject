using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

/// <summary>
///  Fields to use in each table
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// ID information of the table
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }
    /// <summary>
    /// created on information of the table
    /// </summary>
    public DateTime? CreatedOn { get; set; } = DateTime.Now;
    /// <summary>
    ///  created by information of the table
    /// </summary>
    public int? CreatedBy { get; set; }
    /// <summary>
    /// updated on information of the table
    /// </summary>
    public DateTime? UpdatedOn { get; set; }
    /// <summary>
    /// updated by information of the table
    /// </summary>
    public int? UpdatedBy { get; set; }

}